using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;

    public bool gameStopped = false;
    public GameObject canvas;
    public GameObject stopMenu;
    public GameObject lostMenu;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject rightRay;

    void Awake()
    {
        Time.timeScale = 1;
        gameStopped = false;
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
    public void StopGame()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        gameStopped = true;
        canvas.SetActive(true);
        stopMenu.SetActive(true);
        Time.timeScale = 0;
        rightHand.GetComponent<XRDirectInteractor>().enabled = false;
        rightRay.SetActive(true);
        ButtonControllers.instance.QuitTeleport();
    }
    public void LostGame()
    {
        
        canvas.SetActive(true);
        lostMenu.SetActive(true);
        rightHand.GetComponent<XRDirectInteractor>().enabled = false;
        rightRay.SetActive(true);
        Time.timeScale = 0;
        //SceneManager.LoadScene("Level1");
    }
    public void ResumeGame()
    {
        gameStopped = false;
        canvas.SetActive(false);
        stopMenu.SetActive(false);
        rightHand.GetComponent<XRDirectInteractor>().enabled = true;
        rightRay.SetActive(false);
        Time.timeScale = 1;
        
    }
    
    public void loadScene(int index)
    {
        
        SceneManager.LoadScene(index);
    }
    public int GetScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus && Time.timeScale != 0)
        {
            StopGame();
        }
    }
}
