using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMouse : MonoBehaviour
{
    public void EnableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
