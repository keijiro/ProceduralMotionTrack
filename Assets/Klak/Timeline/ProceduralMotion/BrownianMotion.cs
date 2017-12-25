// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class BrownianMotion : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public BrownianMotionPlayable template = new BrownianMotionPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps { get { return ClipCaps.Blending | ClipCaps.Extrapolation; } }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<BrownianMotionPlayable>.Create(graph, template);
        }

        #endregion
    }
}
