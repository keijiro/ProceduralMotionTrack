using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Motion
{
    [System.Serializable]
    public class Offset : PlayableAsset, ITimelineClipAsset
    {
        public OffsetPlayable template = new OffsetPlayable();

        public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<OffsetPlayable>.Create(graph, template);
        }
    }
}
