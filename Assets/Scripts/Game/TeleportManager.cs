using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject rayInteractor;
    [SerializeField] private InputActionProperty teleportModeActivate;
    [SerializeField] private InputActionProperty teleportModeCancel;
    [SerializeField] private InputActionProperty thombMove;
    [SerializeField] private InputActionProperty gripModeActivate;

    // Start is called before the first frame update
    void Start()
    {
        teleportModeActivate.action.Enable();
        teleportModeCancel.action.Enable();
        thombMove.action.Enable();
        gripModeActivate.action.Enable();

        teleportModeActivate.action.performed += OnTeleportActivate;
        teleportModeCancel.action.performed += OnTeleportCancel;
    }

    private void OnTeleportCancel(InputAction.CallbackContext obj)
    {
        rayInteractor.SetActive(false);
    }

    private void OnTeleportActivate(InputAction.CallbackContext obj)
    {
        if(gripModeActivate.action.phase != InputActionPhase.Performed)
        {
            rayInteractor.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
