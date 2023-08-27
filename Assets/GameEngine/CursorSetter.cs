using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    public Texture2D cursorTexture; // The cursor sprite
    public Vector2 cursorHotspot = Vector2.zero; // The cursor hotspot

    void Start()
    {
        // Set the default cursor to the sprite
        
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

}
