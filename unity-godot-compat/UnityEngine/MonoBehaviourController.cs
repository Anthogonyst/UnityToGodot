using Godot;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	public class MonoBehaviourController
	{
		Node referencedNode;

		public List<IEnumerator> coroutines = new List<IEnumerator>();
		bool startCalled;

		public GameObject gameObject { get; private set; }
		public Transform transform { get; private set; }

		public string name { get { return referencedNode.GetName(); } set { referencedNode.SetName(value); } }

		MethodInfo awakeMethod;
		MethodInfo startMethod;
		MethodInfo startMethodCR;
		MethodInfo updateMethod;
		MethodInfo fixedUpdateMethod;
		MethodInfo onEnabledMethod;
		MethodInfo onDisabledMethod;

		const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		VisibilityHandler visibilityHandler;
		bool prevVisibility;


		public MonoBehaviourController(Node node)
		{
			referencedNode = node;
		}


		MethodInfo FindMethod(string methodName, System.Type returnType, System.Type type = null)
		{
			type = type == null ? referencedNode.GetType() : type;
			MethodInfo method = type.GetMethod(methodName, bindingFlags);

			if ( method != null )
			{
				if ( method.GetParameters().Length == 0 )
				{
					if ( method.ReturnType == returnType )
					{
						return method;
					}
				}
			}

			return type == typeof(MonoBehaviour) || type.BaseType == null ? null : FindMethod(methodName, returnType, type.BaseType);
		}


		public void Awake()
		{
			_SetupMonoBehaviour();
		}


		void _SetupMonoBehaviour()
		{
			InitMonoBehaviour();

			if ( awakeMethod != null )
			{
				awakeMethod.Invoke(referencedNode, null);
			}
		}


		void _on_visibility_changed()
		{
			if ( prevVisibility != visibilityHandler.IsVisible )
			{
				bool curVisibility = visibilityHandler.IsVisible;

				if ( curVisibility && onEnabledMethod != null )
				{
					onEnabledMethod.Invoke(referencedNode, null);
				}
				else if ( !curVisibility && onDisabledMethod != null )
				{
					onDisabledMethod.Invoke(referencedNode, null);
				}

				prevVisibility = curVisibility;
			}
		}


		void InitMonoBehaviour()
		{
			if ( referencedNode is Spatial )
			{
				Spatial spatial = referencedNode as Spatial;
				transform = new Transform(spatial);
				visibilityHandler = new SpatialVisibilityHandler(spatial);
			}
			else if ( referencedNode is CanvasItem )
			{
				CanvasItem canvasItem = referencedNode as CanvasItem;
				visibilityHandler = new CanvasItemVisibilityHandler(canvasItem);
			}
			else
			{
				visibilityHandler = new VisibilityHandler();
			}

			gameObject = new GameObject(referencedNode);

			awakeMethod = FindMethod("Awake", typeof(void));
			startMethod = FindMethod("Start", typeof(void));
			startMethodCR = FindMethod("Start", typeof(IEnumerator));
			updateMethod = FindMethod("Update", typeof(void));
			fixedUpdateMethod = FindMethod("FixedUpdate", typeof(void));
			onEnabledMethod = FindMethod("OnEnable", typeof(void));
			onDisabledMethod = FindMethod("OnDisable", typeof(void));
		}


		public void Update()
		{
			try
			{
				if ( !visibilityHandler.IsVisible )
				{
					return;
				}

				if ( !startCalled )
				{
					startCalled = true;

					if ( startMethod != null )
					{
						startMethod.Invoke(referencedNode, null);
					}
					else if ( startMethodCR != null )
					{
						StartCoroutine((IEnumerator)startMethodCR.Invoke(referencedNode, null));
					}
				}

				if ( updateMethod != null )
				{
					updateMethod.Invoke(referencedNode, null);
				}

				for (int i = 0; i < coroutines.Count; i++)
				{
					CustomYieldInstruction yielder = coroutines[i].Current as CustomYieldInstruction;
					bool yielded = yielder != null && yielder.MoveNext();

					if (!yielded && !coroutines[i].MoveNext())
					{
						coroutines.RemoveAt(i);
						i--;
					}
				}
			}
			catch (System.Exception e)
			{
				Debug.Log(e.Message + "\n\n" + e.StackTrace);
			}
		}


		public void FixedUpdate()
		{
			if ( fixedUpdateMethod != null )
			{
				fixedUpdateMethod.Invoke(referencedNode, null);
			}
		}


		public Coroutine StartCoroutine(IEnumerator routine)
		{
			coroutines.Add(routine);
			return new Coroutine(routine);
		}


		public static void Destroy(MonoBehaviour node)
		{
			node.QueueFree();
		}
	}
}