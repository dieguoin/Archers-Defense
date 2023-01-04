using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public Proyectil instance;

    public AudioClip[] audios;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Wall"))
            {
                other.gameObject.GetComponent<AudioSource>().PlayOneShot(audios[0], 1);
            }
            Destroy(gameObject);
        }
    }

}
