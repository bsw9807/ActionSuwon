using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();
        Rect rect = cam.rect;

        float scale_Heigth = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scale_Width = 1 / scale_Heigth;
        if(scale_Heigth < 1)
        {
            rect.height = scale_Heigth;
            rect.y = (1f - scale_Heigth) / 2f;
        }
        else
        {
            rect.width = scale_Width;
            rect.x = (1f - scale_Width) / 2f;
        }
        cam.rect = rect;
    }
}
