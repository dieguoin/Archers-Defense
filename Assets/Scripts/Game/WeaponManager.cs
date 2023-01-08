using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public Weapon currentWeapon;
    
    private Vector3 initialColliderSize;
    private Vector3 initialColliderCenter;

    public Weapon lastWeapon;
    
    ///Arrow///

    public GameObject StringPoint;
    public Transform StringStartingPoint;

    private bool isAttached = false;
    public bool isPulled = false;

    public float maxDistance;
    private float minDistance = 0;
    public float force;

    private float gravity = -9.8f;
    private bool firstLine = false;

    private Vector3 startLine = new Vector3(0.0f, -0.01f, 0.0f);

    private GameObject[] spheres = new GameObject[10];


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

    
    private void Update()
    { 
       
        if (currentWeapon == null)
        {
            return;
        }
        if (currentWeapon.tag == "arrow")
        {
            if (isAttached)
            {
                PullArrow();
            }
            if (isPulled)
            {
                CreatePrediction();
            }
        }
    }



    public void AttachWeapon(Weapon thisWeapon)
    {
        if (currentWeapon != null && currentWeapon.GetComponent<Weapon>().grabbed)
        {
            return;
        }
        if (tag == "leftHand")
        {
            if (!GetComponent<ButtonControllers>().teleportActive)
            {
                currentWeapon = thisWeapon;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;

                currentWeapon.transform.parent = gameObject.transform;

                currentWeapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                switch (currentWeapon.tag)
                {
                    case "arrow":

                        currentWeapon.transform.localRotation = thisWeapon.attachRotation.localRotation;

                        break;
                    case "bow":
                        initialColliderSize = currentWeapon.GetComponent<BoxCollider>().size;
                        initialColliderCenter = currentWeapon.GetComponent<BoxCollider>().center;
                        currentWeapon.GetComponent<BoxCollider>().size = thisWeapon.attachColliderSize;
                        currentWeapon.GetComponent<BoxCollider>().center = thisWeapon.AttachColliderCenter;
                        currentWeapon.GetComponent<BoxCollider>().isTrigger = true;
                        break;
                }

            }
        }
        else
        {

            currentWeapon = thisWeapon;
            currentWeapon.GetComponent<Rigidbody>().useGravity = false;

            currentWeapon.transform.parent = gameObject.transform;

            currentWeapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            switch (currentWeapon.tag)
            {
                case "arrow":

                    currentWeapon.transform.localRotation = thisWeapon.attachRotation.localRotation;

                    break;
                case "bow":
                    initialColliderSize = currentWeapon.GetComponent<BoxCollider>().size;
                    initialColliderCenter = currentWeapon.GetComponent<BoxCollider>().center;
                    currentWeapon.GetComponent<BoxCollider>().size = thisWeapon.attachColliderSize;
                    currentWeapon.GetComponent<BoxCollider>().center = thisWeapon.AttachColliderCenter;
                    currentWeapon.GetComponent<BoxCollider>().isTrigger = true;
                    break;
            }


        }
    }
    public void DettachWeapon()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().useGravity = true;
        switch (currentWeapon.tag)
        {
            case "arrow":
                currentWeapon = null;
                break;

            case "bow":
                currentWeapon.GetComponent<BoxCollider>().size = initialColliderSize;
                currentWeapon.GetComponent<BoxCollider>().center = initialColliderCenter;
                currentWeapon.GetComponent<BoxCollider>().enabled = true;
                currentWeapon.GetComponent<BoxCollider>().isTrigger = false;

                lastWeapon = currentWeapon;
                currentWeapon = null;
                break;
        }
    }


    //////Arrow/////

    

    private void PullArrow()
    {
        if (currentWeapon.tag != "arrow")
        {
            return;
        }
        float dist = Vector3.Project((gameObject.transform.position - StringStartingPoint.position), StringStartingPoint.up).magnitude;
        float frec = (1.0f / 0.3f) * dist;
        OVRInput.SetControllerVibration(frec / dist, 1.0f, OVRInput.Controller.RTouch);
        StringPoint.transform.localPosition = StringStartingPoint.localPosition + new Vector3(-0.02f * (Mathf.Max(minDistance, Mathf.Min(dist, maxDistance))), 0.0f, 0.0f);
        isPulled = true;

    }

    public void AttachBowToArrow()
    {
        
         currentWeapon.transform.parent = StringPoint.transform;
         currentWeapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
         currentWeapon.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
         isAttached = true;
        

    }
    public void DettachBowToArrow()
    {
        
        currentWeapon.GetComponent<Rigidbody>().useGravity = true;

        currentWeapon.transform.parent = gameObject.transform;

        currentWeapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        isAttached = false;
        
    }
    public void ShootArrow()
    {
        if(currentWeapon.tag != "arrow")
        {
            return;
        }
        if (isPulled)
        {
            currentWeapon.transform.parent = null;
            isPulled = false;
            DisableLine();
            isAttached = false;
            currentWeapon.GetComponent<Rigidbody>().AddForce(currentWeapon.transform.up * (StringPoint.transform.localPosition - StringStartingPoint.localPosition).magnitude * -force);
            currentWeapon.GetComponent<Rigidbody>().useGravity = true;
            StringPoint.transform.position = StringStartingPoint.position;
            Destroy(currentWeapon, 5);
            currentWeapon = null;
        }
    }
    public void DisableLine()
    {
        currentWeapon.transform.GetChild(1).GetComponent<LineRenderer>().enabled = false;
    }
    public void CreatePrediction()
    {

        int points = 10;
        LineRenderer lineRenderer = currentWeapon.transform.GetChild(1).GetComponent<LineRenderer>();
        lineRenderer.positionCount = points;

        Vector3 position;
        firstLine = true;
        for (int i = 0; i < points; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(0, 0, 0));
            position = CalculatePosition(i);
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spheres[i].GetComponent<SphereCollider>().enabled = false;
            spheres[i].transform.parent = currentWeapon.transform;
            if (firstLine)
            {
                spheres[i].transform.localPosition = position;
            }
            else
            {
                spheres[i].transform.position = position;
            }

            lineRenderer.SetPosition(i, spheres[i].transform.position);


            firstLine = false;
        }
        lineRenderer.enabled = true;
        for (int i = 0; i < points; i++)
        {
            Destroy(spheres[i]);
        }

    }
    private Vector3 CalculatePosition(int entered)
    {

        Vector3 position;
        Vector3 iniVelocity = currentWeapon.transform.up * Vector3.Distance(StringPoint.transform.localPosition, StringStartingPoint.localPosition) * -force * Time.deltaTime / currentWeapon.GetComponent<Rigidbody>().mass;

        if (firstLine)
        {

            position = startLine;

        }
        else
        {
            position.x = spheres[0].transform.position.x + iniVelocity.x * entered * 0.02f;
            position.y = spheres[0].transform.position.y + iniVelocity.y * entered * 0.02f + (gravity / 2) * Mathf.Pow(entered * Time.deltaTime, 2);
            position.z = spheres[0].transform.position.z + iniVelocity.z * entered * 0.02f;
        }

        return position;
    }
}
