using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChordSpawner))]
[CanEditMultipleObjects]
public class ChordSpawnerInspector : Editor {
    ChordSpawner targetObject;

    private void OnEnable() {
        targetObject = (ChordSpawner)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn")) {
            targetObject.Spawn();
        }
    }
}
