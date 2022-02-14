using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Camera cameraMain;
    [SerializeField] private Unit unit;
    [SerializeField] private bool cursorIsLock;
    // [SerializeField] private PhotonView photon;
    private bool inventIsOopen;

    void Update()
    {
        // if(photon.IsMine)
        // {
            Controlling();
        // }
    }

    private void Start() 
    {
        // if(!photon.IsMine)
        // {
        //     cameraMain.enabled = false;
        // }
        if(cursorIsLock)
        {
            Cursor.lockState = CursorLockMode.Locked;    
        }
        
    }

    private void Controlling()
    {
        if(!unit) return;
        
        InputSystem();  
    }

    private void InputSystem()
    {
        if(unit.Movemant != null)unit.Movemant.MoveAndLook();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(unit.Inventory != null)
            {
                inventIsOopen = unit.Inventory.OpenCloseInventory() ? true : false;
                Cursor.lockState = inventIsOopen ? CursorLockMode.None : CursorLockMode.Locked;

                if(unit.Movemant !=null) unit.Movemant.OnOffLook = !unit.Movemant.OnOffLook;           
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(unit.Inventory != null) unit.Inventory.CheckRay();
        }  
        if(unit.Heands == null || inventIsOopen) return;
        
        if(Input.GetKey(KeyCode.Mouse0))
        {
            
            unit.Heands.Use();
        }

        // if(Input.GetKey(KeyCode.Mouse1) && unit.Heands is Weapon)
        // {
        //     Weapon weapon = (Weapon) unit.Heands;
        //     weapon.Aim();
        // }

        // if(Input.GetKeyDown(KeyCode.R) && unit.Heands is Weapon)
        // {
        //     Weapon weapon = (Weapon)unit.Heands;;
        //     weapon.Reload();
        // }

        unit.Heands.Sway();   
    }
}
