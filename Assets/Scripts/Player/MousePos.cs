using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos
{
    public static Vector2 GetMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
