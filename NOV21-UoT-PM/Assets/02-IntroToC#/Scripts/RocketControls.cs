using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControls : MonoBehaviour
{
    public float thrust = 1000;
    public float dragForce;
    public Rigidbody laserPrefab;
    public Transform spawnPoint;
    public float laserImpulse;
    public Light engineLight;
    public AudioClip laserSound;

    private Rigidbody rocketRigidbody;
    private AudioSource audioSource;
    
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
        // only allowing rocket steering while pressing the mouse right button
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            Vector3 horizontal = new Vector3(0, mouseX * thrust * Time.deltaTime, 0);
            Vector3 vertical = new Vector3(mouseY * thrust * Time.deltaTime, 0, 0);

            rocketRigidbody.AddRelativeTorque(horizontal);
            rocketRigidbody.AddRelativeTorque(vertical);
        }

        bool engineOn = false;

        // move forward
        if (Input.GetKey(KeyCode.W))
        {
            rocketRigidbody.AddForce(transform.forward * thrust * Time.deltaTime);
            engineOn = true;
        }
        // move backwards
        if (Input.GetKey(KeyCode.S))
        {
            rocketRigidbody.AddForce(transform.forward * -thrust * Time.deltaTime);
            engineOn = true;
        }

        // Challenge 1
        // move left
        if (Input.GetKey(KeyCode.A))
        {
            rocketRigidbody.AddForce(transform.right * -thrust * Time.deltaTime);
            engineOn = true;
        }
        // move right
        if (Input.GetKey(KeyCode.D))
        {
            rocketRigidbody.AddForce(transform.right * thrust * Time.deltaTime);
            engineOn = true;
        }

        // Challenge 2
        rocketRigidbody.AddForce(-rocketRigidbody.velocity * dragForce * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }
        engineLight.enabled = engineOn;
    }

    private void FireLaser()
    {
        Rigidbody laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
        laser.velocity = rocketRigidbody.velocity;
        laser.AddForce(transform.forward * laserImpulse);
        audioSource.PlayOneShot(laserSound);
    }
}
