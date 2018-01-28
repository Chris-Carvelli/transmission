using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// socket requires plug
public class SnapSocket : MonoBehaviour {
    //TODO set as private
    public SnapPlug nearPlug;

	SnapPlug connected_plug_;

    // setup in editor
    public Blink Light;

    // null no call, playing=solid light, non null, non playing=calling->blinking
    public Mission CurrentMission;

    public bool HasCallWaiting
    {
        get{
            if(CurrentMission == null) return false;
            return CurrentMission.IsInConversation == false;
        }
    }

    public string Name
    {
        get {return gameObject.name;}
    }
    public override string ToString()
    {
        return this.Name;
    }

    private void UpdateBlinkingLightNotification()
    {
        if(this.Light != null) 
        {
            if(CurrentMission == null)
            {
                // no light
                this.Light.IsBlinking = Blink.BlinkStatus.Off;
            }
            else if(CurrentMission.IsInConversation)
            {
                // light
                this.Light.IsBlinking = Blink.BlinkStatus.On;
            }
            else
            {
                // blink it
                this.Light.IsBlinking = Blink.BlinkStatus.Blink;
            }
        }
    }

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
            Debug.Log(string.Format("Connect called from {0} to {1}", this, value));
			if(value && connected_plug_ != null)
			{
                Debug.Log(string.Format("Clearing {0}", this));
				connected_plug_.SetSocket(null);
			}
			connected_plug_ = value;
			if(connected_plug_ != null)
			{
                // Debug.Log(string.Format("Connected {0}", this));
                connected_plug_.SetSocket(this);
			}
		}
	}

    public Transform outPos;

    private void Update() {
        this.UpdateBlinkingLightNotification();
        if (Input.GetButtonDown("Fire1")) {
            if (ConnectedPlug != null) //something is connected, unplug it
                UnsnapPlug();
            else if (nearPlug != null)//nothing is connected, we can check for the nearest plug
                SnapPlug();
        }
    }

    public void SnapPlug () {
        ConnectedPlug = nearPlug;
    }

    public void UnsnapPlug() {
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
