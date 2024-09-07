using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline {

[CustomEditor(typeof(FollowerMotion)), CanEditMultipleObjects]
class FollowerMotionEditor : Editor
{
    SerializedProperty _target;
    SerializedProperty _interpolator;
    SerializedProperty _positionSpeed;
    SerializedProperty _rotationSpeed;

    static class Styles
    {
        public static readonly GUIContent position = new GUIContent("Position");
        public static readonly GUIContent rotation = new GUIContent("Rotation");
    }

    void OnEnable()
    {
        _target = serializedObject.FindProperty("target");
        _interpolator = serializedObject.FindProperty("template.interpolator");
        _positionSpeed = serializedObject.FindProperty("template.positionSpeed");
        _rotationSpeed = serializedObject.FindProperty("template.rotationSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_target);
        EditorGUILayout.PropertyField(_interpolator);

        EditorGUILayout.LabelField("Speed");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(_positionSpeed, Styles.position);
        EditorGUILayout.PropertyField(_rotationSpeed, Styles.rotation);
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}

} // namespace Klak.Timeline
