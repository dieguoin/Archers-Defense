using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public static Heart instance;
    public GameObject[] hearts;

    private void Awake()
    {
        if(instance == null) { 
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void DestroyHeart(int i)
    {
        
        Destroy(hearts[i]);
    }
}
