using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class ButtonControllers : MonoBehaviour
{
    public static ButtonControllers instance;

    [Header("Teleport")]

    private InputAction teleportActiveAction;
    private InputAction teleportCancelAction;
    private InputAction teleportWeaponAction;

    public bool teleportActive = false;

    [SerializeField] private GameObject rayInteractor;

    [Header("Pause")]
    private InputAction pauseAction;
    [Space] [SerializeField] private InputActionAsset myActionsAsset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    void Start()
    {

        teleportActiveAction = myActionsAsset.FindAction("XRI LeftHand Locomotion/Teleport Mode Activate");
        teleportCancelAction = myActionsAsset.FindAction("XRI LeftHand Locomotion/Teleport Mode Cancel");
        teleportWeaponAction = myActionsAsset.FindAction("XRI LeftHand Interaction/GetWeapon");
        pauseAction = myActionsAsset.FindAction("XRI LeftHand Interaction/Pause");

    }

    private void Update()
    {
        if (teleportActiveAction.triggered)
        {
            OnTeleportActivate();
        }
        if (teleportCancelAction.triggered)
        {
            OnTeleportCancel();
        }
        if (teleportWeaponAction.triggered)
        {
            OnTeleportWeapon();
        }
        if (pauseAction.triggered)
        {
            PauseGame();
        }
    }

    private void OnTeleportWeapon()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        GetComponent<WeaponManager>().lastWeapon.transform.position = gameObject.transform.position;
        GetComponent<WeaponManager>().lastWeapon.transform.rotation = gameObject.transform.rotation;

        GetComponent<WeaponManager>().lastWeapon.transform.localPosition += new Vector3(0, 0, 0.1f);
    }

    private void PauseGame()
    {
        Debug.Log("Pause");
        if (GameManagement.instance.gameStopped)
        {
            GameManagement.instance.ResumeGame();
        }
        else
        {
            GameManagement.instance.StopGame();
        }
    }

    private void OnTeleportCancel()
    {

        QuitTeleport();

    }
    public void QuitTeleport()
    {
        teleportActive = false;
        rayInteractor.SetActive(false);
        GetComponent<XRDirectInteractor>().enabled = true;
    }

    private void OnTeleportActivate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (GetComponent<WeaponManager>().currentWeapon == null)
        {
            teleportActive = true;
            rayInteractor.SetActive(true);
            GetComponent<XRDirectInteractor>().enabled = false;
        }
    }
}
