using System;

namespace UnityEngine
{
	public class Transform
	{
		//public Godot.Spatial spatial;//spatial is renamed to Node3D https://docs.godotengine.org/en/latest/tutorials/3d/introduction_to_3d.html#spatial-node
		public Godot.Node3D node;
		public string name { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
		public GameObject gameObject { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

		//internal Transform(Godot.Spatial node)
		internal Transform(Godot.Node3D node)
		{
			spatial = node;
		}

		public Vector3 position { get { return node.GlobalPosition; } set { node.GlobalPosition = value; } }
		public Vector3 localPosition { get { return node.Position; } set { node.Position = value; } }
		public Quaternion rotation { get { return node.GlobalTransform.Basis.GetRotationQuaternion(); } }//set no equivalent?
		public Quaternion localRotation { get { return node.Quaternion; } set { node.Quaternion = value; } }
		public Vector3 eulerAngles { get { return node.GlobalRotationDegrees; } set { node.GlobalRotationDegrees = value; } }
		public Vector3 localScale { get { return node.Scale; } set { node.Scale = value; } }
		public Vector3 lossyScale { get { return node.GlobalTransform.Basis.Scale; } }

		public Vector3 forward => node.GlobalTransform.Basis.Z;
		public Vector3 up => node.GlobalTransform.Basis.Y;
		public Vector3 right => node.GlobalTransform.Basis.X;

		public void LookAt(Vector3 position, Vector3? up = null) => node.LookAt(position, up);
		public void Rotate(Vector3 eulerAngles)
		{
			node.Rotate(eulerAngles.normalized, eulerAngles.magnitude * Mathf.Deg2Rad);
		}

		public void Translate(Vector3 globalTranslation) => node.GlobalTranslate(globalTranslation);

		public Vector3 TransformPoint(Vector3 localPoint) => node.GlobalTransform * localPoint;

		public Vector3 InverseTransformPoint(Vector3 worldPoint) => worldPoint * node.GlobalTransform;//or  node.GlobalTransform.affine_inverse() * worldPoint

		public Vector3 TransformDirection(Vector3 localDirection) => node.Transform.Basis * localDirection;

		public Vector3 InverseTransformDirection(Vector3 localDirection) => localDirection * node.Transform.Basis;

		public Vector3 TransformVector(Vector3 localVector) => TransformPoint(localVector) - position;


		public T GetComponent<T>()
		{
			throw new NotImplementedException();
		}


		public T GetComponentInChildren<T>()
		{
			throw new NotImplementedException();
		}


		public T[] GetComponentsInChildren<T>()
		{
			throw new NotImplementedException();
		}

	}


}
