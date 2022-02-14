using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player playerSingleton;

    public Transform point;
    public Transform Zombie;
    private Coroutine coroutine;

    private int HP = 100;


    
    //руки игрока в них можно взять все что реализует интерфейс Iholding
    private Iholding heands;
    private IInvent inventory;

    private bool inventoryIsOpen;
    // private WeaponSway weaponSway;
    // private LookWithMouse mouseLook;

    private void Awake() 
    {
        // inventory = GetComponent<IInvent>();

        // mouseLook = Camera.main.GetComponent<LookWithMouse>();

        playerSingleton = this;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        InputSystem();
    }
    
    // взять в руки
    public void TakeInHends(Iholding predmet)
    {
        heands = predmet;
        heands.Take();
    }

    public void PutTheInventory()
    {
        // heands.Hide();
        heands = null;
    }

    private void InputSystem()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            coroutine = StartCoroutine(TestCoroutine());
        } 

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryIsOpen = inventory.OpenCloseInventory();
            // mouseLook.enabled = !mouseLook.enabled;
            Cursor.lockState = inventoryIsOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckRay();
        }  

        if(heands == null || inventoryIsOpen) return;

        if(Input.GetKey(KeyCode.Mouse0))
        {
            heands.Use();
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {
            // Weapon weapon = (Weapon)heands;
            // weapon.Aim();
        }

        // if(Input.GetKeyDown(KeyCode.R) && heands is Weapon)
        {
            // Weapon weapon = (Weapon)heands;
            // weapon.Reload();
        }

        heands.Sway();      
    }

    private void CheckRay()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            // if(hit.transform.GetComponent<ItemWorld>())
            {
                // inventory.AddItem(hit.transform.GetComponent<ItemWorld>().Grab());
            }
        }
    }

    public void GetHit()
    {
        HP -= 20;
        Debug.Log(HP);
        if(HP <= 0 )
        {
            StopGame();
        }
    }

    IEnumerator TestCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Zombie, point.position, Quaternion.identity);
            }
        }
    }

    private void StopGame()
    {
        StopCoroutine(coroutine); 
    }
}
