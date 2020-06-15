using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackHandleGrabbable : OVRGrabbable
{
    [SerializeField] private BackHandle handle;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);
        this.transform.SetPositionAndRotation(handle.transform.position, handle.transform.rotation);
    }
}
