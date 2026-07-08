using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2D : MonoBehaviour
{
    [Header("Ссылки")]
    public Transform target; 

    [Header("Настройки плавности")]
    public float smoothTime = 0.3f; 

    [Header("Смещение")]
    public Vector3 offset = new Vector3(0f, 0f, -10f); 

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

       
        Vector3 targetPosition = target.position + offset;

       
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
