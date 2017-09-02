using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    public class Offset : PlayableAsset, ITimelineClipAsset
    {
        #region Serialized variables

        public OffsetPlayable template = new OffsetPlayable();

        #endregion

        #region ITimelineClipAsset implementation

        public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

        #endregion

        #region PlayableAsset overrides

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<OffsetPlayable>.Create(graph, template);
        }

        #endregion
    }
}
