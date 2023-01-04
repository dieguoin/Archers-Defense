using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [Header("CreateArrow")]
    public GameObject arrowPrefab;
    private float timer = 9;
    private float timerLimit = 10;
    public Transform[] arrowSpawns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerLimit)
        {
            SpawnArrow();
            timer = 0;
        }
    }
    private void SpawnArrow()
    {
        int pos = Random.Range(0, arrowSpawns.Length);
        Instantiate(arrowPrefab, arrowSpawns[pos].position, arrowSpawns[pos].rotation);
    }
}
