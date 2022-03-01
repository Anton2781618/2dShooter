using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2d : MonoBehaviour
{
    public GameObject hitEffect;

    private void Start() 
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == "Player")return;
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);


        Destroy(effect, 5);
        Destroy(gameObject);

    }

}
