using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Motion
{
    [TrackColor(0.9454092f, 0.9779412f, 0.3883002f)]
    [TrackClipType(typeof(Brownian))]
    [TrackBindingType(typeof(Transform))]
    public class BrownianTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<BrownianMixer>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            var transform = director.GetGenericBinding(this) as Transform;
            if (transform == null) return;

            var go = transform.gameObject;
            driver.AddFromName<Transform>(go, "m_LocalPosition.x");
            driver.AddFromName<Transform>(go, "m_LocalPosition.y");
            driver.AddFromName<Transform>(go, "m_LocalPosition.z");
            driver.AddFromName<Transform>(go, "m_LocalRotation.x");
            driver.AddFromName<Transform>(go, "m_LocalRotation.y");
            driver.AddFromName<Transform>(go, "m_LocalRotation.z");
        }
    }
}
