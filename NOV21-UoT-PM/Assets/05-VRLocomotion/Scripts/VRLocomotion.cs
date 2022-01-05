using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRLocomotion : MonoBehaviour
{
    private VRInput vrInput;
    public Transform XRRig;

    public float playerSpeed = 1;

    private LineRenderer lineRenderer;

    private bool isTeleportationEnabled = false;

    public float curveHeight = 2;
    public int curveSegments = 20;

    public float fadeDuration = 1;

    public RawImage blackScreen;


    // Start is called before the first frame update
    void Start()
    {
        vrInput = GetComponent<VRInput>();

        lineRenderer = GetComponent<LineRenderer>();
        //I want it to start disabled
        lineRenderer.enabled = false;
        lineRenderer.positionCount = curveSegments;

    }

    // Update is called once per frame
    void Update()
    {
        HandleRayCast();
        HandleRotation();
        HandleMovement();

    }

    void HandleRayCast()
    {
        //create our ray
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        //If the ray hits somehting 
        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            lineRenderer.enabled = true;


            CalculateCurve(transform.position, hitInfo.point);

            // lineRenderer.SetPosition(0, transform.position);
            // lineRenderer.SetPosition(1, hitInfo.point);


            bool validTarget = hitInfo.collider.CompareTag("Teleportation");

            Color color = validTarget ? Color.blue : Color.red;

            //lineRenderer.startColor = color;
            //lineRenderer.endColor = color;

            lineRenderer.material.color = color;

            if (validTarget && Input.GetButtonDown($"XRI_{vrInput.hand}_TriggerButton"))
            {

                StartCoroutine(FadeAndTeleport(hitInfo.point));

            }


        }
        else // if the ray does no hit something 
        {
            lineRenderer.enabled = false;
        }
    }

    void HandleRotation()
    {
        //Detect if the thumbstick is pressed
        if (Input.GetButtonDown($"XRI_{vrInput.hand}_Primary2DAxisClick"))
        {
            //Detect the direction 
            float rotation = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal") > 0 ? 30 : -30;

            //apply the rotation to the xrrig
            XRRig.Rotate(0, rotation, 0);


        }

    }

    void HandleMovement()
    {

        Vector3 forwardDir = new Vector3(XRRig.forward.x, 0, XRRig.forward.z);
        Vector3 rightDir = new Vector3(XRRig.right.x, 0, XRRig.right.z);

        forwardDir.Normalize();
        rightDir.Normalize();

        float horizontalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Horizontal");
        float verticalValue = Input.GetAxis($"XRI_{vrInput.hand}_Primary2DAxis_Vertical");

        //forward and backwards
        XRRig.position = XRRig.position + (verticalValue * playerSpeed * -forwardDir * Time.deltaTime);

        //right and left
        XRRig.position = XRRig.position + (horizontalValue * playerSpeed * rightDir * Time.deltaTime);






    }

    void CalculateCurve(Vector3 startpoint, Vector3 endPoint)
    {
        Vector3 midPoint = (startpoint + endPoint) / 2;
        Vector3 controlPoint = midPoint + Vector3.up * curveHeight;

        for (int i = 0; i < curveSegments; i++)
        {
            float percent = i / (float)curveSegments;

            Vector3 a = Vector3.Lerp(startpoint, controlPoint, percent);
            Vector3 b = Vector3.Lerp(controlPoint, endPoint, percent);

            Vector3 curvePoint = Vector3.Lerp(a, b, percent);

            lineRenderer.SetPosition(i, curvePoint);

        }

        lineRenderer.SetPosition(curveSegments - 1, endPoint);
    }

    private IEnumerator FadeAndTeleport(Vector3 teleportationPoint)
    {

        float currentTime = 0;


        //Fade to black 
        while (currentTime < fadeDuration)
        {

            blackScreen.color = Color.Lerp(Color.clear, Color.black, currentTime);

            yield return new WaitForEndOfFrame();
            currentTime = currentTime + Time.deltaTime;

        }

        blackScreen.color = Color.black;
        //Teleport
        XRRig.position = teleportationPoint;

        //wait a little bit 
        yield return new WaitForSeconds(1);


        //Fade from black
        currentTime = 0;

        while (currentTime < fadeDuration)
        {

            blackScreen.color = Color.Lerp(Color.black, Color.clear, currentTime);

            yield return new WaitForEndOfFrame();
            currentTime = currentTime + Time.deltaTime;

        }

        blackScreen.color = Color.clear;









    }


}
