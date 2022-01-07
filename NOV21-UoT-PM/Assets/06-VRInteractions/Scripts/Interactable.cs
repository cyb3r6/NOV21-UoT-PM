using UnityEngine;
public abstract class Interactable : GrabbableObject
{

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (controller != null)
        {
            if (Input.GetButtonDown($"XRI_{controller.hand}_TriggerButton"))
            {
                Interact();
            }
        }


    }

    protected abstract void Interact();

}
