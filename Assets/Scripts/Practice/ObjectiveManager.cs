using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] const float SPAWNTIME = 2;
    private float timer;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject objective;
    private void Update()
    {
        if (timer < 0)
        {
            timer = SPAWNTIME;
            GameObject newObjective = Instantiate(objective, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), objective.transform.rotation);
            newObjective.transform.rotation = Quaternion.LookRotation(player.position - newObjective.transform.position);
        }
        timer -= Time.deltaTime;
    }
}
