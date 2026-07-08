using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private Camera mainCamera;

    void Start()
    {
        // Находим главную камеру на сцене
        mainCamera = Camera.main;
        
        
        // Получаем ссылку на Rigidbody 2D
        rb = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        RotateTowardsCursor();
        
        // Считываем нажатия (WASD / стрелки)
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        // Перемещаем физическое тело в 2D пространстве
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


    

    

    void RotateTowardsCursor()
    {
        
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

       
        mouseWorldPosition.z = 0f;

        
        Vector3 lookDirection = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        
       
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}
