using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRInput : MonoBehaviour
{
    [Header("Hand")]
    public Hand hand = Hand.Left;

    public float gripValue;

    public UnityEvent OnGripDown;
    public UnityEvent OnGripUp;

    private string gripAxis;
    private string gripButton;

    
    void Start()
    {
        gripAxis = $"XRI_{hand}_Grip";
        gripButton = $"XRI_{hand}_GripButton";
    }

    
    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);

        if (Input.GetButtonDown(gripButton))
        {
            OnGripDown?.Invoke();
        }
        if (Input.GetButtonUp(gripButton))
        {
            OnGripUp?.Invoke();
        }
    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}