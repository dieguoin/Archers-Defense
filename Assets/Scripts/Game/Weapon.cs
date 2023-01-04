using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Attach Info")]
    public Transform attachRotation;
    public Vector3 attachColliderSize;
    public Vector3 AttachColliderCenter;
    protected GameObject hand;

    [Header("Audio")]
    public AudioClip[] audios;
    protected AudioSource controlAudio;

    public bool grabbed = false;

    void Awake()
    {
        attachRotation = this.gameObject.transform.GetChild(0).transform;
        controlAudio = GetComponent<AudioSource>();
    }

    public void CreateWeapon()
    {
        hand.GetComponent<WeaponManager>().AttachWeapon(this);
        grabbed = true;

    }
    public void LeaveWeapon()
    {

        hand.GetComponent<WeaponManager>().DettachWeapon();
        grabbed = false;
    }

    public void SelectionAudio(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }
}
