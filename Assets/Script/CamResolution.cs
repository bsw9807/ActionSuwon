using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();
        Rect rect = cam.rect;

        float scale_Height = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scale_Width = 1 / scale_Height;
        if (scale_Height < 1)
        {
            rect.height = scale_Height;
            rect.y = (1f - scale_Height) / 2f;
        }
        else
        {
            rect.width = scale_Width;
            rect.x = (1f - scale_Width) / 2f;
        }
        cam.rect = rect;
    }
}
