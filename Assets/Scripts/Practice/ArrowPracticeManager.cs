using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPracticeManager : MonoBehaviour
{

    [SerializeField] private GameObject ArrowPrefab;
    private float timer = 0;
    private int MaxTimer = 5;
    private void Update()
    {
        if(timer > MaxTimer)
        {
            timer = 0f;
            Respawn();
        }
        timer += Time.deltaTime;
    }
    private void Respawn()
    {
        GameObject arrow = Instantiate(ArrowPrefab, transform);
        arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}