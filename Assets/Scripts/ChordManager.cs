using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ChordManager : MonoBehaviour {
    private float _length = 1;
    public float Length {
        get { return _length; }
        set {
            _length = value;

            foreach (var plug in plugs)
                plug.SetLength(_length);
            //TODO add proper conversion between Unity unit and ObiRope lenght
            if (Application.isPlaying && cursor != null) {
                cursor.ChangeLength(_length);
            }
        }
    }

    private ObiRopeCursor cursor;
    PlugManager[] plugs;

    private void Awake() {
        cursor = GetComponentInChildren<ObiRopeCursor>();
        plugs = GetComponentsInChildren<PlugManager>();
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }
}
