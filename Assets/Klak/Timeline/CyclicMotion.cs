// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class CyclicMotion : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public CyclicMotionPlayable template = new CyclicMotionPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps { get { return ClipCaps.Blending | ClipCaps.Extrapolation; } }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<CyclicMotionPlayable>.Create(graph, template);
        }

        #endregion
    }
}
