using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Motion
{
    [System.Serializable]
    public class Brownian : PlayableAsset, ITimelineClipAsset
    {
        public BrownianPlayable template = new BrownianPlayable();

        public ClipCaps clipCaps { get { return ClipCaps.Blending; } }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return ScriptPlayable<BrownianPlayable>.Create(graph, template);
        }
    }
}
