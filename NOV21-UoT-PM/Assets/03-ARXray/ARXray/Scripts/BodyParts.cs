using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyParts : MonoBehaviour
{
    public Image brainIcon;
    public Image heartIcon;
    public Image leftLungIcon;
    public Image rightLungIcon;

    public void OnBrainFound()
    {
        brainIcon.color = new Color(1,1,1,1);
    }

    public void OnHeartFound()
    {
        heartIcon.color = new Color(1, 1, 1, 1);
    }
    public void OnLeftLungFound()
    {
        leftLungIcon.color = new Color(1, 1, 1, 1);
    }
    public void OnRightLungFound()
    {
        rightLungIcon.color = new Color(1, 1, 1, 1);
    }

}
