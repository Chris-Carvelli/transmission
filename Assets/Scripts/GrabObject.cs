using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col) {
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        collidingObject = col.gameObject;
    }

    // Update is called once per frame
    void Update() {
        if (Controller.GetHairTriggerDown()) {
            if (collidingObject) {
                GrabObj();
            }
        }

        if (Controller.GetHairTriggerUp()) {
            if (objectInHand) {
                ReleaseObject();
            }
        }
    }

    private void GrabObj() {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        Rigidbody body = objectInHand.GetComponent<Rigidbody>();
        body.useGravity = false;
        joint.connectedBody = body;

        SnapPlug plug = objectInHand.GetComponent<SnapPlug>();

        if (plug != null)
            plug.UnplugginAction();

        SnapPhone phone = objectInHand.GetComponent<SnapPhone>();

        if (phone != null)
            phone.UnplugginAction();
    }

    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject() {
        objectInHand.GetComponent<Rigidbody>().useGravity = true;
        if (GetComponent<FixedJoint>()) {

            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            SnapPlug plug = objectInHand.GetComponent<SnapPlug>();
            SnapPhone phone = objectInHand.GetComponentInChildren<SnapPhone>();
            if (plug != null) {
                if (plug.nearSocket != null) {
                    if (plug.nearSocket.ConnectedPlug == null) {
                        plug.nearSocket.nearPlug = plug;
                        plug.nearSocket.SnapPlug();

                    }
                }
            }
            else if (phone != null) {
                if (phone.phoneBase != null) {
                    phone.PlugginAction();
                }
            }
            else {
                objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            }
        }
        objectInHand = null;
    }


    public void OnTriggerEnter(Collider other) {
        //if (other.gameObject.GetComponent<Grabbable>())
            SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other) {
        //if (other.gameObject.GetComponent<Grabbable>())
            SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other) {
        /*if (!other.gameObject.GetComponent<Grabbable>())
            return;*/

        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }
}
