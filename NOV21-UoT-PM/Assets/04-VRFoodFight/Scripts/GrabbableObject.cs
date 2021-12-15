using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Rigidbody grabbableRigidbody;
    public Renderer grabbableRenderer;
    public Color hoverColor;
    public Color nonHoverColor;

    void Start()
    {
        
    }

    public void OnHoverStart()
    {
        grabbableRenderer.material.color = hoverColor;
    }

    public void OnHoverEnd()
    {
        grabbableRenderer.material.color = nonHoverColor;
    }
   
    public void ParentGrab(VRInput controller)
    {
        transform.SetParent(controller.transform);
        grabbableRigidbody.useGravity = false;
        grabbableRigidbody.isKinematic = true;
    }
    public void ParentRelease()
    {
        transform.SetParent(null);
        grabbableRigidbody.useGravity = true;
        grabbableRigidbody.isKinematic = false;
    }
    public void JointGrab()
    {

    }
    public void JointRelease()
    {

    }
}
