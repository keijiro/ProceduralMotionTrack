// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using Klak.Math;

namespace Klak.Timeline
{
    [System.Serializable]
    public class BrownianMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 positionAmount = Vector3.one;
        public Vector3 rotationAmount = Vector3.one * 10;
        public float frequency = 1;
        public int octaves = 2;
        public int randomSeed;

        #endregion

        #region PlayableBehaviour overrides

        Vector3 _positionOffset;
        Vector3 _rotationOffset;

        public override void OnPlayableCreate(Playable playable)
        {
            var hash = new XXHash(randomSeed);

            _positionOffset = new Vector3(
                hash.Range(-1e3f, 1e3f, 0),
                hash.Range(-1e3f, 1e3f, 1),
                hash.Range(-1e3f, 1e3f, 2)
            );

            _rotationOffset = new Vector3(
                hash.Range(-1e3f, 1e3f, 0),
                hash.Range(-1e3f, 1e3f, 1),
                hash.Range(-1e3f, 1e3f, 2)
            );
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target == null) return;

            var t = (float)playable.GetTime() * frequency;
            var w = info.weight / 0.75f; // normalized weight

            var np = new Vector3(
                Perlin.Fbm(_positionOffset.x + t, octaves),
                Perlin.Fbm(_positionOffset.y + t, octaves),
                Perlin.Fbm(_positionOffset.z + t, octaves)
            );

            var nr = new Vector3(
                Perlin.Fbm(_rotationOffset.x + t, octaves),
                Perlin.Fbm(_rotationOffset.y + t, octaves),
                Perlin.Fbm(_rotationOffset.z + t, octaves)
            );

            np = Vector3.Scale(np, positionAmount) * w;
            nr = Vector3.Scale(nr, rotationAmount) * w;

            target.localPosition += np;
            target.localRotation = Quaternion.Euler(nr) * target.localRotation;
        }

        #endregion
    }
}
