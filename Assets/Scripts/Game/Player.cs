using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    private int livePoints;
    public AudioClip[] audios;
    private AudioSource controlAudio;


    private void Awake()
    {
        if(instance == null)
        {
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

    private void Start()
    {
        controlAudio = GetComponent<AudioSource>();
        livePoints = 3;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectile"))
        {
            DamageTaken();
        }
    }
    public void DamageTaken()
    {
        controlAudio.PlayOneShot(audios[0], 1.0f);
        livePoints -= 1;

        Heart.instance.DestroyHeart(livePoints);

        if (livePoints <= 0)
        {
            GameManagement.instance.LostGame();
        }
    }
}
