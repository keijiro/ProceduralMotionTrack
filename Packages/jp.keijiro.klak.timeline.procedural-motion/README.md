ProceduralMotionTrack
=====================

![gif](https://i.imgur.com/jlE9XbR.gif)

**ProceduralMotionTrack** is a custom Unity Timeline track that controls an
object’s transform using simple procedural motions.

Currently, it supports five types of motion:

- **Constant Motion**: Fixes an object at a specified position.
- **Cyclic Motion**: Moves an object using sine wave functions.
- **Brownian Motion**: Moves an object with fractal Brownian motion.
- **Follower Motion**: Makes an object follow another target.
- **Jitter Motion**: Moves an object with random values.

These motions can be blended by overlapping clips.

ProceduralMotionTrack also supports extrapolation, which is useful for creating
continuous motion throughout the entire timeline.

System Requirements
-------------------

- Unity 6

Installation
------------

[Follow these instructions] to set up the scoped registry. Then, install the
ProceduralMotionTrack package via the Unity Package Manager.

[Follow these instructions]:
  https://gist.github.com/keijiro/f8c7e8ff29bfe63d86b888901b82644c
