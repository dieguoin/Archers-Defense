using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawners;

    private GameObject currentEnemy;
    public GameObject enemyPrefav;

    void Update()
    {
        if (currentEnemy == null)
        {
            int pos = Random.Range(0, spawners.Length);
            currentEnemy = Instantiate(enemyPrefav);
            currentEnemy.transform.parent = gameObject.transform;

            enemyPrefav.transform.position = spawners[pos].position;


        }
    }
    
    
}
