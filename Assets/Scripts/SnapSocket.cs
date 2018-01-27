using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// socket requires plug
public class SnapSocket : MonoBehaviour {
    //TODO set as private
    public SnapPlug nearPlug;

	SnapPlug connected_plug_;

	// the conencted plug
	// TODO: add some events?
	public SnapPlug ConnectedPlug
	{
		get
		{
			return connected_plug_;
		}

		set
		{
			if(value && connected_plug_ != null)
			{
				connected_plug_.SetSocket(null);
			}
			connected_plug_ = value;
			if(connected_plug_ != null)
			{
				connected_plug_.SetSocket(this);
			}
		}
	}

    public Transform outPos;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (ConnectedPlug != null) //something is connected, unplug it
                UnsnapPlug();
            else if (nearPlug != null)//nothing is connected, we can check for the nearest plug
                SnapPlug();
        }
    }

    private void SnapPlug () {
        ConnectedPlug = nearPlug;
    }

    private void UnsnapPlug() {
        ConnectedPlug.UnplugginAction();
        ConnectedPlug = null;
    }

    public Transform GetStartTransform () {
        return outPos;
    }

    public Transform GetEndTransform() {
        return transform;
    }

    private void OnTriggerEnter(Collider other) {
        SnapPlug plug = other.gameObject.GetComponent<SnapPlug>();

        if (plug == null)
            return;

        nearPlug = plug;
    }

    private void OnTriggerExit(Collider other) {
        SnapPlug plug = other.gameObject.GetComponent<SnapPlug>();

        if (plug == null)
            return;

        nearPlug = null;
    }
}
