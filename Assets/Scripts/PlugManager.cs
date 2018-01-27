using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugManager : MonoBehaviour {
    private ConfigurableJoint _joint;

    private void Awake() {
        _joint = GetComponent<ConfigurableJoint>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLength (float length) {
        _joint.linearLimit = new SoftJointLimit {
            limit = length
        };
    }
}
