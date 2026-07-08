using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairTexture; // Сюда перетащите текстуру

    void Start()
    {
       
        Cursor.visible = true;
       
        Cursor.lockState = CursorLockMode.Confined;


        
        Vector2 hotspot = new Vector2(crosshairTexture.width / 2f, crosshairTexture.height / 2f);

        
        Cursor.SetCursor(crosshairTexture, hotspot, CursorMode.Auto);
    }

}
