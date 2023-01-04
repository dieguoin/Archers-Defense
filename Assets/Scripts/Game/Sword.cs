using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground" && !grabbed)
        {
            SelectionAudio(1, 1.0f);
        }
        if (other.tag == "enemy" && grabbed)
        {
            SelectionAudio(2, 1.0f);
        }
        
    }

}
