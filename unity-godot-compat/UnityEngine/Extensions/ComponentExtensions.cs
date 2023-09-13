using Godot;
using System.Collections;
using System.Collections.Generic;


namespace UnityEngine
{
	public static class ComponentExtensions
	{
		public static T AddComponent<T>(this Node node) where T : Node, new()
		{
			T newNode = new T();

			node.AddChild(newNode);

			return newNode;
		}


		public static T GetComponent<T> (this Node node) where T : Node
		{
			return node as T;
		}


		public static T GetComponentInParent<T> (this Node node) where T : Node
		{
			Node curNode = node;

			do
			{
				if ( node is T )
				{
					return node as T;
				}

				node = node.GetParent();
			}
			while ( node != null );

			return null;
		}


		public static T[] GetComponentsInParent<T> (this Node node) where T : Node
		{
			Node curNode = node;
			List<T> components = new List<T>();

			do
			{
				if ( node is T )
				{
					components.Add(node as T);
				}

				node = node.GetParent();
			}
			while ( node != null );

			return components.ToArray();
		}


		public static T GetComponentInChildren<T> (this Node node) where T : Node
		{
			return FindChild<T>(node);
		}


		static T FindChild<T>(Node parent) where T : Node
		{
			int childCount = parent.GetChildCount();

			if ( parent is T )
			{
				return parent as T;
			}

			if ( childCount > 0 )
			{
				for ( int i = 0; i < childCount; i++ )
				{
					T node = FindChild<T>(parent.GetChild(i));

					if ( node != null )
					{
						return node;
					}
				}
			}

			return null;
		}


		public static T[] GetComponentsInChildren<T> (this Node node) where T : Node
		{
			List<T> components = new List<T>();
			CollectChildComponents<T>(node, components);
			return components.ToArray();
		}


		static void CollectChildComponents<T>(Node parent, List<T> components) where T : Node
		{
			int childCount = parent.GetChildCount();

			if ( parent is T )
			{
				components.Add(parent as T);
			}

			if ( childCount > 0 )
			{
				for ( int i = 0; i < childCount; i++ )
				{
					CollectChildComponents<T>(parent.GetChild(i), components);
				}
			}
		}


		public static Coroutine StartCoroutine(this Node node, IEnumerator routine)
		{
			MonoBehaviourController controller = UnityEngineAutoLoad.Instance.GetMonoBehaviourController(node);

			if ( controller != null )
			{
				controller.coroutines.Add(routine);
			}

			return new Coroutine(routine);
		}


		public static void Destroy (this Object obj)
		{
			obj.Free();
		}
	}
}