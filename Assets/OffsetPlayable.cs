using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Motion
{
    [System.Serializable]
    public class OffsetPlayable : PlayableBehaviour
    {
        public Vector3 position;
        public Vector3 rotation;
        [Range(0, 1)] public float amplitude = 1;
    }
}
