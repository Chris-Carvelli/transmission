using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChordManager))]
[CanEditMultipleObjects]
public class ChordManagerInspector : Editor {
    ChordManager targetObject;

    private void OnEnable() {
        targetObject = (ChordManager)target;
    }

    public override void OnInspectorGUI() {
        float _length = targetObject.Length;
        //base.OnInspectorGUI();

        targetObject.Length = EditorGUILayout.FloatField("Length", targetObject.Length);
    }
}
