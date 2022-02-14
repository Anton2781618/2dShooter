using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс поведение выстрелами
public class shooting2d : MonoBehaviour, IFirearms, Iholding
{
    public Transform firePoint;
    public GameObject buletPrefab;
    public Canvas canvas;

    [SerializeField] private float fireRate;

    private float nextTimeToFire = 0f;

    public float bulettForce = 20f;

    private void Start() 
    {
        canvas = FindObjectOfType<Canvas>();
        Unit unit = FindObjectOfType<Unit>();
        unit.TakeInHends(this);
    }

    private void shooting()
    {
        GameObject bulett = Instantiate(buletPrefab, firePoint.position, firePoint.rotation);
        bulett.transform.SetParent(canvas.transform);
        Rigidbody2D rb = bulett.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulettForce, ForceMode2D.Impulse);
    }

    public void Fire()
    {
        
        if(Time.time < nextTimeToFire) return;

        shooting();
        nextTimeToFire = Time.time + 1f / fireRate;
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        Fire();
    }

    public void Take()
    {
        Debug.Log("взял в руки оружие");
    }

    public void Throw()
    {
        throw new System.NotImplementedException();
    }

    public void Sway()
    {
        Debug.Log("Sway");
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }
}
