using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(Offset))]
    class OffsetEditor : Editor
    {
        SerializedProperty _position;
        SerializedProperty _rotation;

        SerializedProperty _noiseFrequency;
        SerializedProperty _noiseOctaveCount;

        SerializedProperty _amplitude;
        SerializedProperty _envelope;

        SerializedProperty _randomSeed;

        static class Styles
        {
            static public readonly GUIContent octaveCount = new GUIContent("Octave Count");
        }

        void OnEnable()
        {
            _position = serializedObject.FindProperty("template.position");
            _rotation = serializedObject.FindProperty("template.rotation");

            _noiseFrequency = serializedObject.FindProperty("template.noiseFrequency");
            _noiseOctaveCount = serializedObject.FindProperty("template.noiseOctaveCount");

            _amplitude = serializedObject.FindProperty("template.amplitude");
            _envelope = serializedObject.FindProperty("template.envelope");

            _randomSeed = serializedObject.FindProperty("template.randomSeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_position);
            EditorGUILayout.PropertyField(_rotation);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_noiseFrequency);
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider(_noiseOctaveCount, 0, 9, Styles.octaveCount);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.Slider(_amplitude, 0, 1);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_envelope);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_randomSeed);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
