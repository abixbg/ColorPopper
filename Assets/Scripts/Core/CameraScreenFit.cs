using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CameraScreenFit : MonoBehaviour
{
    public float fitX;
    public float2 ScreenSafeArea;
    public float OrtoSize;

    private void Update()
    {
        ScreenSafeArea = new float2(Screen.safeArea.height, Screen.safeArea.width);

        OrtoSize = fitX * ScreenSafeArea.x / ScreenSafeArea.y * 0.5f;

        Camera.main.orthographicSize = OrtoSize;
    }
}
