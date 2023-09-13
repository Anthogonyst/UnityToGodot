using System;
using Godot;
using System.Reflection;

namespace UnityEngine
{
	public class GameObject
    {
		Node _node;


		public string name
		{
			get { return _node.GetName(); }
			set { _node.SetName(value); }
		}

		public Transform transform { get; private set; }

		public int layer { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


		internal GameObject(Node node)
		{
			_node = node;
		}


		public GameObject()
		{
			_node = new Node();
			name = "Game Object";
			UnityEngineAutoLoad.Instance.AddChild(_node);
		}


		public GameObject(string name)
		{
			_node = new Node();
			this.name = name;
			UnityEngineAutoLoad.Instance.AddChild(_node);
		}


		public void SetActive(bool active)
		{
			PropertyInfo visibleProperty = _node.GetType().GetProperty("Visible");

			if ( visibleProperty != null )
			{
				visibleProperty.SetValue(_node, active);
			}
		}


		public static Node Find(string name)
		{
			Node root = UnityEngineAutoLoad.Instance.GetTree().GetRoot();
			return FindChild(root, name);
		}


		static Node FindChild(Node parent, string name)
		{
			int childCount = parent.GetChildCount();

			if ( parent.GetName().Equals(name) )
			{
				return parent;
			}

			if ( childCount > 0 )
			{
				for ( int i = 0; i < childCount; i++ )
				{
					Node node = FindChild(parent.GetChild(i), name);

					if ( node != null )
					{
						return node;
					}
				}
			}

			return null;
		}


		public T GetComponent<T>() where T : Node, new()
		{
			return _node.GetComponent<T>();
		}


		public T AddComponent<T>() where T : Node, new()
		{
			return _node.AddComponent<T>();
		}


		//
		// Conversion Methods
		//
		public static implicit operator Node (GameObject gameObject)
		{
			return gameObject._node;
		}


		public static implicit operator GameObject (Node node)
		{
			return new GameObject(node);
		}
	}
}