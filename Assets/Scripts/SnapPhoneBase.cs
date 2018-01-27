using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPhoneBase : MonoBehaviour {
    public Transform endTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetEndTransform () {
        return endTransform;
    }
}
