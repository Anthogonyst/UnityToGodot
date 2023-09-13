using System.Collections.Generic;
using UnityEngine;

namespace Godot
{
    // Class names must match exactly

    class Node : Godot.Object
    {
        public string name;
        public List<Node> children = new List<Node>();

        public void AddChild(Node newChild)
        {
            string uniqueName = newChild.name;
            bool conflict = true;
            int i = 1;

            while(conflict)
            {
                conflict = false;
                foreach(var existingChild in children)
                {
                    if(existingChild.name == uniqueName)
                    {
                        conflict = true;
                        ++i;
                        uniqueName = newChild.name + i;
                        break;
                    }
                }
            }

            newChild.name = uniqueName;
            children.Add(newChild);
        }
    }

    class CanvasItem : Node
    {
        public Color selfModulate = Color.white;
        public bool visible = true;

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d.Add("self_modulate", selfModulate);
            d.Add("visible", visible);
            return d;
        }
    }

    class Node2D : CanvasItem
    {
        public Vector2 position;
        public Vector2 scale = new Vector2(1, 1);
        public float rotation;

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d.Add("position", position);
            d.Add("scale", scale);
            d.Add("rotation", rotation);
            return d;
        }
    }

    class Sprite : Node2D
    {
        public Texture texture;

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d.Add("texture", texture);
            return d;
        }
    }

    class CollisionObject2D : Node2D
    { }

    class PhysicsBody2D : Node2D
    {
        public int collisionLayer = 1;

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d.Add("collision_layer", collisionLayer);
            return d;
        }
    }

    class KinematicBody2D : PhysicsBody2D
    { }

    class RigidBody2D : PhysicsBody2D
    { }

    class CollisionShape2D : Node2D
    { }

    class Camera2D : Node2D
    {
        public bool current = false;

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d.Add("current", current);
            return d;
        }
    }

    class Control : CanvasItem
    { }

    class Spatial : Node
    { }
}
