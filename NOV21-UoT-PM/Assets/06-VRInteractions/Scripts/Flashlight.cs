using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Interactable
{

    [SerializeField]
    [Tooltip("Attach the light that will be controlled here")]
    private Light light;

    protected override void Start()
    {
        base.Start();

    }
    protected override void Interact()
    {

        light.enabled = !light.enabled;
    }

}
