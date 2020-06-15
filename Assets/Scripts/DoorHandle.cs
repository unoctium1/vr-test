using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] private Transform target = default;

    [SerializeField] private Transform handle = default;

    [SerializeField] private Rigidbody doorPanel = default;

    [SerializeField, Tooltip("At this rotation, lock handle and unlock door")]
    private float maxHandleRotation = -60f;

    [SerializeField] float resetSpeed = 2f;

    [SerializeField] AudioSource sfx = default;

    private Rigidbody rb;

    private bool isDoorLocked = true;
    private bool isHandleResetting = false;

    public bool IsGrabbed { get; set; } = false;

    private float currentHandlePosition;
    private float deltaHandlePosition;


    //public bool isTargetGrabbed { get; set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(maxHandleRotation < 0)
        {
            maxHandleRotation = 360 + maxHandleRotation;
        }
        doorPanel.isKinematic = true; //ensure door can't move
        currentHandlePosition = target.transform.position.y;
    }
    private void FixedUpdate()
    {
        if (!IsGrabbed && !isDoorLocked && Quaternion.Angle(doorPanel.transform.rotation, Quaternion.identity) < 5f)
        {
            doorPanel.transform.rotation = Quaternion.identity;
            doorPanel.isKinematic = true;
            isDoorLocked = true;
            sfx.Play();
        }

        float curAngle = handle.localRotation.eulerAngles.x;
        if (curAngle <= 0 || curAngle > maxHandleRotation)
        {
            float lastPos = currentHandlePosition;
            currentHandlePosition = target.transform.position.y;
            deltaHandlePosition = currentHandlePosition - lastPos;
            if (deltaHandlePosition < 0)
            {
                // Get really rough approximate of where handle position should be
                float angle = Mathf.Asin(deltaHandlePosition) * 180f / Mathf.PI;
                handle.Rotate(Vector3.right, 4 * angle);
            }
        }

        if (isDoorLocked)
        {
            if (!isHandleResetting && handle.localRotation.eulerAngles.x < maxHandleRotation && deltaHandlePosition < 0)
            {
                //Debug.Log("Open Door");
                sfx.Play();
                isDoorLocked = false;
                doorPanel.isKinematic = false;
            }

        }
        else
        {
            rb.MovePosition(target.position);
        }
    }

    public void ResetHandlePos()
    {
        StartCoroutine(ResetHandle());
        // if the door is open, and the handle was dropped when the door is in a locked position, reset to closed state
        
    }

    private IEnumerator ResetHandle()
    {
        isHandleResetting = true;
        while (handle.transform.rotation != Quaternion.identity)
        {

            handle.transform.localRotation = Quaternion.Lerp(handle.transform.localRotation, Quaternion.identity, resetSpeed * Time.deltaTime);
            if(Quaternion.Angle(handle.transform.rotation, Quaternion.identity) < 1f)
            {
                handle.transform.localRotation = Quaternion.identity;
                isHandleResetting = false;
            }
            yield return null;
        }
        yield return null;
    }
    
    /*
    private void LateUpdate()
    {
        //Ensure constraints are enforced and handle only rotates around x axis
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
    }
    */
    
}
