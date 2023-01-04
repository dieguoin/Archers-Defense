using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private float time;
    private float timeShowed = 3;

    private bool showed = true;

    // Update is called once per frame
    private void Update()
    {

        if (showed)
        {
            time += Time.deltaTime;
            if (time > timeShowed)
            {
                showed = false;
                GetComponent<Text>().text = null;
            }
        }
    }
    public void ShowLivePoints(int ps)
    {
        GetComponent<Text>().text = ps.ToString();
        showed = true;
        time = 0;
    }
}
