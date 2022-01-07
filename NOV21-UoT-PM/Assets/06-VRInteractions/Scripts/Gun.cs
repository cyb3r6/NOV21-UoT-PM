using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Interactable
{
    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    protected float force = 2000;


    protected override void Interact()
    {
        GameObject bulletClone = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.forward * force);

    
    }
}
