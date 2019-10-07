// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(ConstantMotion)), CanEditMultipleObjects]
    class ConstantMotionEditor : Editor
    {
        SerializedProperty _position;
        SerializedProperty _rotation;

        void OnEnable()
        {
            _position = serializedObject.FindProperty("template.position");
            _rotation = serializedObject.FindProperty("template.rotation");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_position);
            EditorGUILayout.PropertyField(_rotation);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
