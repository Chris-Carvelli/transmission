using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPhone : Grabbable {
    public GameObject sparkEffect;

    public SnapPhoneBase phoneBase;

    private Rigidbody _body;

    private void Awake() {
        _body = GetComponent<Rigidbody>();
    }

    private void Start() {
        PlugginAction();
    }

    public void PlugginAction() {
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);
        Transform t = phoneBase.GetEndTransform();

        _body.MovePosition(t.position);
        Quaternion q = t.rotation;
        q.eulerAngles = new Vector3(0, 0, 0);
        _body.MoveRotation(q);

        _body.useGravity = false;
        

        //sparking
        Instantiate(sparkEffect).transform.position = transform.position;
    }

    public void UnplugginAction() {
        //_body.AddForce(InSocket.GetEndPosition() - transform.position);

        if (phoneBase != null) {

            Transform t = phoneBase.GetEndTransform();
            //phoneBase.ConnectedPlug = null;

            _body.MovePosition(t.position);
        }
        _body.useGravity = true;

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
