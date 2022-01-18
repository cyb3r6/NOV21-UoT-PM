using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTouchController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float turnSpeed = 5f;
    public float deadzone = 0.2f;

    private Animator robotAnim;
    private Rigidbody robotRigidbody;
    private Joystick joystick;
    
    void OnEnable()
    {
        joystick = FindObjectOfType<Joystick>();
        robotRigidbody = GetComponent<Rigidbody>();
        robotAnim = GetComponent<Animator>();

        robotAnim.SetBool("Open_Anim", true);
    }

    
    void Update()
    {
        // handling movement
        if(joystick.Direction.magnitude >= deadzone)
        {
            robotRigidbody.AddForce(transform.forward * moveSpeed);

            // set the robot animator to walking
            robotAnim.SetBool("Walk_Anim", true);
        }
        else
        {
            robotAnim.SetBool("Walk_Anim", false);
        }

        // handle rotation
        Vector3 targetDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        Vector3 direction = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turnSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
