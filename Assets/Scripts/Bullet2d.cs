using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2d : MonoBehaviour
{
    public GameObject hitEffect;
    public Canvas canvas;

    private void Start() 
    {
        canvas = FindObjectOfType<Canvas>();
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.transform.SetParent(canvas.transform);

        // other.transform.GetComponent<SpiderMovemant2d>().GetHit();

        Destroy(effect, 5);
        Destroy(gameObject);
    }

}
