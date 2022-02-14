using System.Collections;
using System.Collections.Generic;
// using Photon.Pun;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{

    public Iholding Heands { get; set; }
    public IInvent Inventory { get; set; }
    public IUnitMovemant Movemant { get; set ; }
    public int Health { get; set; } = 100;
    // public PhotonView photon;

    private void Awake() 
    {        
        Inventory = GetComponent<IInvent>(); 
        Movemant = GetComponent<IUnitMovemant>();         
    }

    // взять в руки
    public void TakeInHends(Iholding predmet)
    {
        Heands = predmet;
        Heands.Take();
    }    

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Healing(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
