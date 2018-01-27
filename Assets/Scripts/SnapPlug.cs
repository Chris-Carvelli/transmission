﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// plug is inserted into socket
public class SnapPlug : Grabbable {
    public GameObject sparkEffect;
    public SnapSocket nearSocket;

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

        Quaternion q = t.rotation;
        q.eulerAngles = new Vector3(0, 0, 270);
        _body.MoveRotation(q);

        _body.MovePosition(t.position);

        _body.useGravity = false;
        _body.velocity = Vector3.zero;



        //sparking
        Instantiate(sparkEffect).transform.position = transform.position;
        _body.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnplugginAction() {
        Debug.Log("unplug");
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);
        
        if (InSocket != null) {

            Transform t = InSocket.GetStartTransform();
            InSocket.ConnectedPlug = null;

            _body.MovePosition(t.position);
        }
        _body.useGravity = true;
        _body.constraints = RigidbodyConstraints.None;

    }


    private void OnTriggerEnter(Collider other) {
        SnapSocket socket = other.gameObject.GetComponent<SnapSocket>();

        if (socket == null)
            return;

        nearSocket = socket;
    }

    private void OnTriggerExit(Collider other) {
        SnapSocket socket = other.gameObject.GetComponent<SnapSocket>();

        if (socket == null)
            return;

        nearSocket = null;
    }
}
