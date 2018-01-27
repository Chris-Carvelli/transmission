using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSpawner : MonoBehaviour {
    public ChordManager chordPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn (float length = 0.5f) {
        Instantiate<ChordManager>(chordPrefab).Length = length;

    }
}
