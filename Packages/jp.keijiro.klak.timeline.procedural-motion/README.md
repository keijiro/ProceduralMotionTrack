ProceduralMotionTrack
=====================

![gif](https://i.imgur.com/jlE9XbR.gif)

This is an implementation of a custom timeline track that controls an object transform with simple procedural motions.

At the moment, it has four types of motions:

- Constant Motion: Fix an object to a specified point.
- Cyclic Motion: Move an object with sine wave functions.
- Brownian Motion: Move an object with a fractal Brownian motion.
- Jitter Motion: Move an object with random values.

These motions can be blended by overlapping clips.

Procedural Motion track supports extrapolation that is useful to give an infinite motion during an entire timeline.

System Requirements
-------------------

- Unity 2017.3 or later

License
-------

[Unlicense](https://unlicense.org/)
