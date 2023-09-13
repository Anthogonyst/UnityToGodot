# UnityToJSON

![alt tag](https://raw.githubusercontent.com/ThunderBeastGames/UnityToJSON/master/ExportToJSON.png)

### What?

**UnityToJSON** is a Unity scene to JSON file format exporter.  It currently exports the following to a single convenient JSON file which is easy to process:

 * Nodes (aka GameObjects)
 * Components
 * Lights
 * Meshes
 * Skeletal Meshes
 * Materials
 * Shaders
 * Textures
 * Lightmaps
 * Animation Clips
 * Physics
 * Terrain
 * Cameras

### Example Uses

**JSON** is a standard and highly useful format for processing tasks.

* Export to the [Atomic Game Engine](http://www.AtomicGameEngine.com/) (supports JSON import natively)

* Export for use with renderers such as [Three.js](http://threejs.org/)

* Export to tooling which analyzes scenes and reports model statistics, etc


### Example JSON: Empty Scene

This is some sample output of the default empty Unity scene.  The entire scene is exported to a single convenient JSON file with binary data such as textures, lightmaps, etc encoded to Base64

```
{
  "name": "EmptyScene",
  "resources": {
    "textures": [],
    "lightmaps": [],
    "shaders": [],
    "materials": [],
    "meshes": []
  },
  "hierarchy": [
    {
      "name": "Main Camera",
      "components": [
        {
          "localPosition": [
            0.0,
            1.0,
            -10.0
          ],
          "localRotation": [
            0.0,
            0.0,
            0.0,
            1.0
          ],
          "localScale": [
            1.0,
            1.0,
            1.0
          ],
          "name": null,
          "parentName": null,
          "type": "Transform"
        },
        {
          "type": "Camera"
        }
      ],
      "children": []
    },
    {
      "name": "Directional Light",
      "components": [
        {
          "localPosition": [
            0.0,
            3.0,
            0.0
          ],
          "localRotation": [
            0.408217937,
            -0.234569728,
            0.109381676,
            0.875426054
          ],
          "localScale": [
            1.0,
            1.0,
            1.0
          ],
          "name": null,
          "parentName": null,
          "type": "Transform"
        },
        {
          "color": [
            1.0,
            0.956862748,
            0.8392157,
            1.0
          ],
          "range": 10.0,
          "lightType": "Point",
          "castsShadows": true,
          "realtime": false,
          "type": "Light"
        }
      ],
      "children": []
    }
  ]
}
```
