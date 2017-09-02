using UnityEngine;
using UnityEngine.Playables;
using Klak.Math;

namespace Klak.Motion
{
    [System.Serializable]
    public class BrownianPlayable : PlayableBehaviour
    {
        #region Exposed variables

        public float frequency = 0.1f;
        [Range(1, 9)] public int fractalLevel = 3;

        public Vector3 positionAmplitude = new Vector3(0.1f, 0.1f, 0.1f);
        public Vector3 rotationAmplitude = new Vector3(4, 4, 0);

        [Range(0, 1)] public float totalAmplitude = 1;

        public int randomSeed;

        #endregion

        #region Private constants

        const float _fbmNorm = 1 / 0.75f;

        #endregion

        #region Public functions

        public Vector3 CalculatePosition(float time)
        {
            var hash = new XXHash(randomSeed);
            var t = time * frequency;
            var tx = hash.Range(-1e3f, 1e3f, 0) + t;
            var ty = hash.Range(-1e3f, 1e3f, 1) + t;
            var tz = hash.Range(-1e3f, 1e3f, 2) + t;

            var n = new Vector3(
                Perlin.Fbm(tx, fractalLevel),
                Perlin.Fbm(ty, fractalLevel),
                Perlin.Fbm(tz, fractalLevel)
            );

            var amp = positionAmplitude * totalAmplitude * _fbmNorm;
            return Vector3.Scale(n, amp);
        }

        public Vector3 CalculateRotation(float time)
        {
            var hash = new XXHash(randomSeed);
            var t = time * frequency;
            var tx = hash.Range(-1e3f, 1e3f, 3) + t;
            var ty = hash.Range(-1e3f, 1e3f, 4) + t;
            var tz = hash.Range(-1e3f, 1e3f, 5) + t;

            var n = new Vector3(
                Perlin.Fbm(tx, fractalLevel),
                Perlin.Fbm(ty, fractalLevel),
                Perlin.Fbm(tz, fractalLevel)
            );

            var amp = rotationAmplitude * totalAmplitude * _fbmNorm;
            return Vector3.Scale(n, amp);
        }

        #endregion
    }
}
