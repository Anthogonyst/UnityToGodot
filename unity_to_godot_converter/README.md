Unity Engine to Godot Engine exporter
https://github.com/Zylann/unity_to_godot_converter/tree/master
=======================================

This is an experimental script that allows you to convert all scenes in your Unity project into a Godot project. It is not aimed at doing everything automatically, only things that can be converted decently.
It's only a proof of concept on simple 2D games for now, and a ton of work remains to be done if it were to support everything else.
While there are always cases where conversion is ambiguous and things to do manually, it's still fun to at least have the ability to automate this to some extent.

I have other projects to work on so I won't work much on this tool for now, and I am aware that there is an abysmal amount of features it could support^^ But feel free to hack around with it and improve it if you like the idea.


How to install
---------------

Copy this repository in your Unity project, inside a folder named `Editor`, and you should see a new `Godot` menu with options in it.

Although it should not modify anything in the project, it's up to you to preserve your data if anything wrong happens :p


Some challenges
-----------------

Here is a random list of things I had to take choices, for which workarounds may or may not exist.
There may be a lot more, but you can get an idea of what this tool has to get through:

- Unity only has `Camera`, but Godot has `Camera2D` and `Camera` for 3D. Choosing which one is ambiguous, so for now I create the 2D version of the camera is orthographic AND if a hint is enabled in the exporter for 2D projects. Also, in Unity, cameras also act as viewports, which is another separate node in Godot, so I'm not sure how to even convert those. Other components are ambiguous too, such as `Light`.

- Godot has separate engines for 2D and 3D, but Unity only has 3D transforms with ortho camera. So the tool tries to guess what usage a GameObject is for by looking at its components. For example, if it has `SpriteRenderer`, or any of its children does, then the GameObject is converted to a `Node2D`. Otherwise, it becomes a `Spatial`. In some cases, it becomes a blank `Node` in cases where dimensions are irrelevant.

- Unity uses components attached to GameObjects for its functionality, but Godot uses a node tree directly. That means a single GameObject with several components may convert into one node and several child nodes. If a GameObject only has a Transform and one component, a shortcut is taken to only produce a single Godot node, eliminating the unnecessary nesting.

- Unity defines rigidbodies as components, but in Godot it is recommended to have such bodies as parent nodes because they control the position of their children, so instead of adding `RigidBodies` as a child nodes, they are have to be promoted as parent.

- Unity can have multiple scripts on the same GameObject, but Godot can only have one per node. So the converter takes the first script it finds to the root node, and create children `Nodes` for each additional script. You may have to have a manual look after conversion if you use composition a lot.

- Converting scripts is very complicated, so the tool rather creates stub scripts for each of them so it can still attach them to the proper final nodes, and attempts to preserve serialized variables. For example, when converting to GDScript, a C# script will be parsed for its variables which will be written as `export` on top of an empty GDScript, and the rest or the original source code is written as a big comment below them. This allows to keep configurations and keep track of what the script should be.

- In Sprite texture resources, Unity allows to define a scaling between pixel coordinates and world coordinates, which is 100 by default, making sprites very small. Godot uses pixels as units at all times, so the plugin attempts to undo this scaling.

- Unity can subdivide a 2D texture into sprites, so this almost always translates to Godot as `AtlasTextures`.

- Unity uses a left-handed coordinate system, and in 2D its Y axis stays upwards. In Godot, the Y axis in 2D is downwards, so the tool attempts to invert positions (not working as best as it could at the moment)

- Godot has no terrain system as of now, but a plugin exists for heightmaps which does not require recompilation. So the plugin could be packaged in the output project, and Unity terrains could be mostly converted to that format.

- Things requiring a recompilation of Godot cannot be supported, for example the Admob module needs to be integrated into Godot manually by recompiling the engine.

- Unity and Godot both support prefabs and nested prefabs, but I haven't worked in this part yet. On Unity side it should be a matter of using `PrefabUtility` to detect if a game object is actually an instance of a prefab, and it needs some research to see which delta-modifications are supported both by Unity and Godot.

- As of 3.1 Godot only saves non-default values in scene data, but this tool can't afford to know them all, so scenes generated by it may be larger than if you had created them in Godot. Saving them from Godot might get rid of the redundancy.

- Unity can imports 3D models as "fixed" prefabs, a bit like Godot does, so I am not sure if the tool should generate scenes for those, or let Godot do it

