using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline {

[System.Serializable]
public sealed class FollowerMotion : PlayableAsset, ITimelineClipAsset
{
    #region Serialized attributes

    public ExposedReference<Transform> target;
    public FollowerMotionPlayable template = new FollowerMotionPlayable();

    #endregion

    #region ITimelineClipAsset implementation

    public ClipCaps clipCaps => ClipCaps.Blending | ClipCaps.Extrapolation;

    #endregion

    #region PlayableAsset overrides

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var p = ScriptPlayable<FollowerMotionPlayable>.Create(graph, template);
        p.GetBehaviour().Target = target.Resolve(graph.GetResolver());
        return p;
    }

    #endregion
}

} // namespace Klak.Timeline
