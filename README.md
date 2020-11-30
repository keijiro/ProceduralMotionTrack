ProceduralMotionTrack
=====================

![gif](https://i.imgur.com/jlE9XbR.gif)

This is an implementation of a custom timeline track that controls an object
transform with simple procedural motions.

At the moment, it has four types of motions:

- Constant Motion: Fix an object to a specified point.
- Cyclic Motion: Move an object with sine wave functions.
- Brownian Motion: Move an object with a fractal Brownian motion.
- Jitter Motion: Move an object with random values.

These motions can be blended by overlapping clips.

Procedural Motion track supports extrapolation that is useful to give an
infinite motion during an entire timeline.

System Requirements
-------------------

- Unity 2019.4 or later

How To Install
--------------

This package uses the [scoped registry] feature to resolve package
dependencies. Please add the following sections to the manifest file
(Packages/manifest.json).

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html

To the `scopedRegistries` section:

```
{
  "name": "Keijiro",
  "url": "https://registry.npmjs.com",
  "scopes": [ "jp.keijiro" ]
}
```

To the `dependencies` section:

```
"jp.keijiro.klak.timeline.procedural-motion": "1.0.1"
```

After changes, the manifest file should look like below:

```
{
  "scopedRegistries": [
    {
      "name": "Keijiro",
      "url": "https://registry.npmjs.com",
      "scopes": [ "jp.keijiro" ]
    }
  ],
  "dependencies": {
    "jp.keijiro.klak.timeline.procedural-motion": "1.0.1",
    ...
```
