using Godot;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace UnityEngine
{
	public class UnityEngineAutoLoad : Node
	{
		static UnityEngineAutoLoad instance = null;
		public static UnityEngineAutoLoad Instance
		{
			get
			{
				if ( instance == null )
				{
					instance = new UnityEngineAutoLoad();
				}

				return instance;
			}
		}


		List<Node> monoNodes = new List<Node>();
		Dictionary<Node, MonoBehaviourController> monoBehaviours = new Dictionary<Node, MonoBehaviourController>();


		public UnityEngineAutoLoad()
		{
			instance = this;
		}


		public override void _Ready()
		{
			GetTree().Connect("node_added", this, "_node_added");

			InitialScan();
		}


		void InitialScan()
		{
			CheckNode(GetTree().GetRoot());
		}


		void CheckNode(Node currentNode)
		{
			UseAsMonoBehaviour attr = currentNode.GetType().GetCustomAttribute<UseAsMonoBehaviour>();

			Debug.Log("Node: " + currentNode.GetName());

			if ( attr != null )
			{
				monoNodes.Add(currentNode);
				monoBehaviours[currentNode] = new MonoBehaviourController(currentNode);
				monoBehaviours[currentNode].Awake();
			}

			for ( int i = 0; i < currentNode.GetChildCount(); i++ )
			{
				CheckNode(currentNode.GetChild(i));
			}
		}


		void _node_added(Node node)
		{
			Debug.Log(node.GetName() + " added");

			CheckNode(node);
		}


		public override void _Process(float delta)
		{
			Time.time += delta;
			Time.deltaTime = delta;

			foreach ( Node node in monoNodes )
			{
				monoBehaviours[node].Update();
			}
		}


		public override void _PhysicsProcess(float delta)
		{
			Time.fixedDeltaTime = delta;

			foreach ( Node node in monoNodes )
			{
				monoBehaviours[node].FixedUpdate();
			}
		}


		internal MonoBehaviourController GetMonoBehaviourController(Node node)
		{
			if ( monoBehaviours.ContainsKey(node) )
			{
				return monoBehaviours[node];
			}

			return null;
		}


		public void Quit()
		{
			SceneTree tree = GetTree();

			if ( tree != null )
			{
				tree.Quit();
			}
		}
	}
}