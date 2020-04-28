using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    Rect crosshairRect;
    Texture crosshairTexture;

    void Start()
    {
        float crosshairSize = Screen.width * 0.1f;
        crosshairTexture = Resources.Load ("Textures/crosshair") as Texture;
        crosshairRect = new Rect(Screen.width / 2 - crosshairSize / 2,
            Screen.height / 2 - crosshairSize / 2,
            crosshairSize, crosshairSize);
    }

    void OnGUI()
    {
        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }
}
