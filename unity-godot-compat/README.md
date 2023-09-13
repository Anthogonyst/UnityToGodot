# UnityGodotCompat
https://github.com/NathanWarden/unity-godot-compat
An implementation of the C# UnityEngine namespace that works in Godot

# What To Expect
The goal of this compatibility library is to try and make a 1 to 1 way to import code you have for the Unity Engine and be able to just start using it in Godot.

Unfortunately, while this library will get you close, there are a few things you need to set up first, and there are also a few things that simply don't translate directly from Unity to Godot. For instance, since Godot uses a hierachy of Nodes, and each node works the same way as a Component in Unity, the GetComponents method doesn't make much sense because there will always only be a single component per Node, since the Node itself is the component. However, GetComponentsInChildren still makes a lot of sense because you can have multiple nodes of a particular type that are children of each node.

**If there's something missing that you need please do one of the following:**

1) Open an issue and provide a sample MonoBehaviour from Unity (in a zip file preferably). If you don't provide a sample I will likely not be able to help you as my time is limited.
2) Fork this repo, implement it yourself and make a pull request. It will be very appreciated!

# Setup
The only thing that is currently needed to set up this library for use is that you need to create an AutoLoad C# class that derives from UnityEngineAutoLoad, add it to your .csproj project, then add it to the AutoLoad tab under Project > Project Settings > AutoLoad

Here's an example of the C# class you need to create.

``` C#
public class MyUnityEngineAutoLoad : UnityEngine.UnityEngineAutoLoad
{
  // No code needed here
}
```

Next, to setup your MonoBehaviour you'll likely need to add the `UseAsMonoBehaviour` attribute and change the class to derive from the Node type that you're using.

For instance, if you have the following class in Unity:

``` C#
using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    public float rotationSpeed = 30.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
```

Since it operates in 3D and will require a Spatial node you'll need to change it to the following:

``` C#
using UnityEngine;

[UseAsMonoBehaviour]
public class ObjectSpinner : Spatial
{
    public float rotationSpeed = 30.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
```

**Notice that all we did was *add the attribute* [UseAsMonoBehaviour] and change `MonoBehaviour` to `Spatial`**

There are some cases where you will literally need to change nothing at all. A simple example of this would be a frames per second counter. A class like this doesn't require any special node type. Therefore, it can still derive from `MonoBehaviour` since it derives directly from `Node`.


# Contributing
For now, the only rules for contributing are:
1) Follow the normal casing rules for C# except for the sake of Unity compatibility. For instance properties start with a capital letter, however many UnityEngine classes contain properties that start with a lowercase letter. In these cases use a lowercase letter.
2) Indent using tabs, not spaces
3) Use uncuddled curly brackets
4) If you aren't sure about any other styling issues, try and follow the general styling of the MonoBehaviour class as this is almost exclusively custom code.
5) If you aren't sure about something, please open an issue and ask :)

And... thanks!
