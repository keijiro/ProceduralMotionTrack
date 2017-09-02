using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class Motion : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public MotionPlayable template = new MotionPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps {
            get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
        }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<MotionPlayable>.Create(graph, template);
        }

        #endregion
    }
}
