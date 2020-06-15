using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGrabbable : OVRGrabbable
{
    [SerializeField] private DoorHandle handle;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        handle.IsGrabbed = true;
        //handle.isTargetGrabbed = true;
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);
        handle.IsGrabbed = false;
        this.transform.SetPositionAndRotation(handle.transform.position, handle.transform.rotation);
        handle.ResetHandlePos();
        //handle.isTargetGrabbed = false;
    }
}
