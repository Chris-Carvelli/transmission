using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPhone : Grabbable {
    public SnapPhoneBase phoneBase;
    public SnapPlug otherSide;

    private Rigidbody _body;

    public bool snapped = true;
    public Vector3 snappedPos;
    public Quaternion snappedRot;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    private void Start() {
        PlugginAction();
    }

    private void Update() {
        if (snapped) {

            _body.velocity = Vector3.zero;
            _body.angularVelocity = Vector3.zero;

            transform.position = snappedPos;
            transform.rotation = snappedRot;
        }
    }

    public void PlugginAction() {
        snapped = true;
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);
        Transform t = phoneBase.GetEndTransform();
        snappedPos = t.position;
        snappedRot = t.rotation;

        _body.MovePosition(snappedPos);
        Quaternion q = t.rotation;
        //q.eulerAngles = new Vector3(0, 0, 0);
        _body.MoveRotation(snappedRot);

        _body.useGravity = false;
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
        //_body.constraints = RigidbodyConstraints.FreezeAll;

        GetComponent<AudioSource>().Stop();
    }

    public void UnplugginAction() {
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);

        if (phoneBase != null) {

            Transform t = phoneBase.GetEndTransform();
            //phoneBase.ConnectedPlug = null;

            _body.MovePosition(t.position);
        }
        _body.constraints = RigidbodyConstraints.None;
        _body.useGravity = true;
        snapped = false;

        GetComponent<AudioSource>().Play();
    }


    private void OnTriggerEnter(Collider other) {
        SnapPhoneBase pb = other.gameObject.GetComponent<SnapPhoneBase>();

        if (pb == null)
            return;

        phoneBase = pb;
    }

    private void OnTriggerExit(Collider other) {
        SnapPhoneBase pb = other.gameObject.GetComponent<SnapPhoneBase>();

        if (pb == null)
            return;

        phoneBase = null;
    }
}
