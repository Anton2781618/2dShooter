using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform crossHair;
    [SerializeField] private GameObject buletPrefab;

    private Camera cam;
    private Vector2 mouseWorldPosition;

    private void Awake() 
    {
        cam = Camera.main;
    }

    void Update()
    {
        AnimationProcess();
        MoveCrossHair();    
        Fire();
    }

    private void FixedUpdate() 
    {
        Movemant();
    }

    private void AnimationProcess()
    {
        
        Vector2 aim = (crossHair.transform.position - transform.position).normalized;
        // animator.SetFloat("horizontal", movement.x);
        // animator.SetFloat("vertical", movement.y);
        // animator.SetFloat("magnetuda", movement.magnitude);
        // Vector2 go = new Vector2(0, 0);
        // if(Input.GetKey(KeyCode.S))
        // {
        //     go.y = -3;
        // }
        // if(Input.GetKey(KeyCode.W))
        // {
        //     go.y = 3;
        // }
        // if(Input.GetKey(KeyCode.A))
        // {
        //     go.x = -3;
        // }
        // if(Input.GetKey(KeyCode.D))
        // {
        //     go.x = 3;
        // }
        animator.SetFloat("horizontal", aim.x );
        animator.SetFloat("vertical", aim.y );
        animator.SetFloat("magnetuda", aim.magnitude);

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 6;
    }
    private void Movemant()
    {
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(axisX ,axisY, 0.0f); 
        transform.position = transform.position + movement * speed * Time.deltaTime;
        
        float a = 0;
        if(axisX > 0 || axisY > 0)
        {
            a = 1;
        }

        if(axisX < 0 || axisY < 0)
        {
            a = -1;
        }
        

        animator.SetFloat("isgo", a);
    }

    private void MoveCrossHair()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.position = mouseWorldPosition;
    }

    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 heading = (crossHair.transform.position - transform.position).normalized;
            var bullet = Instantiate(buletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = heading * bulletSpeed;
            bullet.transform.Rotate(0, 0, Mathf.Atan2(mouseWorldPosition.y, mouseWorldPosition.x) * Mathf.Rad2Deg);
            Destroy(bullet, 5);
        }
    }
}
