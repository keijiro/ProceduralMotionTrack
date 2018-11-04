// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class BrownianAnimation : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public BrownianAnimationPlayable template = new BrownianAnimationPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps { get { return ClipCaps.Blending | ClipCaps.Extrapolation; } }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<BrownianAnimationPlayable>.Create(graph, template);
        }

        #endregion
    }
}
