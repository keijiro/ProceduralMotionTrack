using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(Motion))]
    class MotionEditor : Editor
    {
        SerializedProperty _positionOffset;
        SerializedProperty _rotationOffset;

        SerializedProperty _positionNoise;
        SerializedProperty _rotationNoise;

        SerializedProperty _noiseFrequency;
        SerializedProperty _noiseOctaveCount;

        SerializedProperty _amplitude;
        SerializedProperty _envelope;

        SerializedProperty _randomSeed;

        static class Styles
        {
            static public readonly GUIContent position = new GUIContent("Position");
            static public readonly GUIContent rotation = new GUIContent("Rotation");
            static public readonly GUIContent frequency = new GUIContent("Frequency");
            static public readonly GUIContent octaveCount = new GUIContent("Octave Count");
            static public readonly GUIContent curve = new GUIContent("Curve");
        }

        void OnEnable()
        {
            _positionOffset = serializedObject.FindProperty("template.positionOffset");
            _rotationOffset = serializedObject.FindProperty("template.rotationOffset");

            _positionNoise = serializedObject.FindProperty("template.positionNoise");
            _rotationNoise = serializedObject.FindProperty("template.rotationNoise");

            _noiseFrequency = serializedObject.FindProperty("template.noiseFrequency");
            _noiseOctaveCount = serializedObject.FindProperty("template.noiseOctaveCount");

            _amplitude = serializedObject.FindProperty("template.amplitude");
            _envelope = serializedObject.FindProperty("template.envelope");

            _randomSeed = serializedObject.FindProperty("template.randomSeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Offset", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_positionOffset, Styles.position);
            EditorGUILayout.PropertyField(_rotationOffset, Styles.rotation);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Noise", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_positionNoise, Styles.position);
            EditorGUILayout.PropertyField(_rotationNoise, Styles.rotation);
            EditorGUILayout.PropertyField(_noiseFrequency, Styles.frequency);
            EditorGUILayout.IntSlider(_noiseOctaveCount, 1, 9, Styles.octaveCount);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.Slider(_amplitude, 0, 1);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_envelope, Styles.curve);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_randomSeed);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
