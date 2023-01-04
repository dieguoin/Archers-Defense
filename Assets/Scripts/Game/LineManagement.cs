using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManagement : MonoBehaviour
{


    public LineRenderer lr;
    public Transform[] points;


    private void Awake() {

       
        lr = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++) {

            lr.SetPosition(i, points[i].position);
        }
    }
}
