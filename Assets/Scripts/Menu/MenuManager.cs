using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
