using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ChordManager : MonoBehaviour {
    //TMP public property to avoid custom editor
    public float length = 1.0f;

    private ObiRopeCursor cursor;

    private void Awake() {
        cursor = GetComponentInChildren<ObiRopeCursor>();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(cursor.rope.RestLength + ":" + length);
        cursor.ChangeLength(length);
    }
}
