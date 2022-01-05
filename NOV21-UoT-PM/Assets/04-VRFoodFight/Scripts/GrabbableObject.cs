using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class GrabbableObject : MonoBehaviour
{
    public Rigidbody grabbableRigidbody;
    public Renderer grabbableRenderer;

    public Color hoverColor;
    public Color nonHoverColor;


    void Start()
    {
        grabbableRigidbody = GetComponent<Rigidbody>();
        grabbableRenderer = GetComponent<Renderer>();
    }

    public void OnHoverStart()
    {
        grabbableRenderer.material.color = hoverColor;
    }

    public void OnHoverEnd()
    {
        grabbableRenderer.material.color = nonHoverColor;
    }
   
    public void  ParentGrab(VRInput controller)
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
 
}
