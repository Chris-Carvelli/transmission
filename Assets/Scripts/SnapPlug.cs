using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// plug is inserted into socket
public class SnapPlug : MonoBehaviour {

	// set in unity editor
	public SnapPlug OtherSide;

	// the socket this is connected to (only for reading)
	// TODO: add some event handling?
	public SnapSocket InSocket
	{
		get;
		private set;
	}

	// called from snap socket
	public void SetSocket(SnapSocket socket)
	{
		this.InSocket = socket;
        PlugginAction();
	}

    private Rigidbody _body;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
    }
    
    public void PlugginAction() {
        Debug.Log("plug");
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);
        Transform t = InSocket.GetEndTransform();

        _body.MovePosition(t.position);
        _body.MoveRotation(t.rotation);
    }

    public void UnplugginAction() {
        Debug.Log("unplug");
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);
        Transform t = InSocket.GetStartTransform();

        _body.MovePosition(t.position);
    }
}
