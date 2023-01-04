using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    public Collider bc;
    private bool inside = false;
    private bool shot = false;

    private bool quivered1 = false;
    private bool quivered2 = false;


    public bool GetGrabbed()
    {
        return grabbed;
    }
    void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
        SelectionAudio(0, 1);

    }
    
    void OnTriggerEnter(Collider other){

        if ((other.tag == "rightHand" || other.tag == "leftHand") && !grabbed)
        {
            hand = other.gameObject;
        }

        if (other.tag == "bow"){
         
            if (grabbed)
            {
                inside = true;
            } 
        }
        if (other.tag == "wall" || other.tag == "floor") {
            
            if (shot)
            {
                
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
                bc.enabled = false;
            }
        }
        
        if(other.tag == "enemy")
        {
            SelectionAudio(1, 1.0f);
            Destroy(gameObject);
        }
        if(other.tag == "Quiver1")
        {
            quivered1 = true;
        }
        if(other.tag == "Quiver2")
        {
            quivered2 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.tag == "bow")
        {
            
            inside = false;
            if (shot) {
                bc.isTrigger = true;
            }
        }
        if (other.tag == "Quiver1")
        {
            quivered1 = false;
        }
        if (other.tag == "Quiver2")
        {
            quivered2 = false;
        }
    }
    public void PlaceArrow()
    {
        if (inside) {
            hand.GetComponent<WeaponManager>().AttachBowToArrow();
            
        }
    }
    public void UnplaceArrow() {
        
        if (hand.GetComponent<WeaponManager>().isPulled)
        {
            shot = true;
            grabbed = false;
            hand.GetComponent<WeaponManager>().ShootArrow();
            Destroy(gameObject, 5.0f);
        }
    }
    public void CreateArrow(){

        bc.isTrigger = true;

        hand.GetComponent<WeaponManager>().AttachWeapon(this);
        grabbed = true;

    }
    public void ReleaseArrow() {
        if (!shot)
        {
            if (quivered1 && quivered2)
            {
                SaveArrow();
            }
            else
            {
                bc.isTrigger = false;
                hand.GetComponent<WeaponManager>().DettachWeapon();
                grabbed = false;
            }
        }
    }
    private void SaveArrow()
    {
        Quiver.instance.AddArrow();
        Destroy(gameObject);
    }
    public void SelectionAudio(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }

    

}
