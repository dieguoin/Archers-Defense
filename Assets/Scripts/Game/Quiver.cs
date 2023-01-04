using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public static Quiver instance;

    [SerializeField] private GameObject arrowPrefab;
    private GameObject thisArrow;
    public int arrowNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    public void AddArrow()
    {
        arrowNumber++;
    }
    public void GetArrow()
    {
        arrowNumber--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "rightHand" || other.tag == "leftHand") && other.gameObject.GetComponent<WeaponManager>().currentWeapon == null  && arrowNumber > 0)
        {
            thisArrow = GameObject.Instantiate(arrowPrefab, other.transform);
            thisArrow.GetComponent<BoxCollider>().isTrigger = true;
            thisArrow.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "rightHand" || other.tag == "leftHand") && thisArrow != null && arrowNumber > 0)
        {
            thisArrow.transform.position = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "rightHand" || other.tag == "leftHand") && arrowNumber > 0)
        {
            if (!thisArrow.GetComponent<Arrow>().GetGrabbed() && thisArrow != null)
            {
                Destroy(thisArrow);
            }
            else
            {
                GetArrow();
            }
        }
        
    }

}
