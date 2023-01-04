using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if ((other.tag == "rightHand" || other.tag == "leftHand") && !grabbed)
        {
            hand = other.gameObject;
        }
        if (other.tag == "ground" && !grabbed)
        {
            SelectionAudio(0, 1.0f);
        }
    }

}
