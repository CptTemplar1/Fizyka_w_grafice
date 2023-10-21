using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    void Awake()
    {
        // W��cz kursor
        Cursor.visible = true;

        // Odblokuj kursor
        Cursor.lockState = CursorLockMode.None;
    } 
}
