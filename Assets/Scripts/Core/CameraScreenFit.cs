using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CameraScreenFit : MonoBehaviour
{
    public Board board;

    public float unitsPerPixel;
    public Transform quad;
    public float fitX;
    public float2 ScreenSafeArea;
    public float OrtoSize;
    // Set this to your target aspect ratio, eg. (16, 9) or (4, 3).
    public Vector2 targetAspect = new Vector2(16, 9);
    private Camera _camera;

    public float gameWidth;

    private void Update()
    {
        //Fit();
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        board.OnBoardChanged += HandleBoardChanged;
    }

    private void OnDisable()
    {
        board.OnBoardChanged -= HandleBoardChanged;
    }

    private void Fit()
    {
        ScreenSafeArea = new float2(Screen.safeArea.height, Screen.safeArea.width);

        OrtoSize = fitX * ScreenSafeArea.x / ScreenSafeArea.y * 0.5f;

        _camera.orthographicSize = OrtoSize;

        gameWidth = _camera.aspect * _camera.orthographicSize * 2f;
    }

    private void HandleBoardChanged()
    {
        ZoomTo(board.BoardRect.x);
    }

    private void ZoomTo(float width)
    {
        fitX = width;
        Fit();
    }


    // Call this method if your window size or target aspect change.
    public void UpdateCrop()
    {
        // Determine ratios of screen/window & target, respectively.
        float screenRatio = Screen.width / (float)Screen.height;
        float targetRatio = targetAspect.x / targetAspect.y;

        if (Mathf.Approximately(screenRatio, targetRatio))
        {
            // Screen or window is the target aspect ratio: use the whole area.
            _camera.rect = new Rect(0, 0, 1, 1);
        }
        else if (screenRatio > targetRatio)
        {
            // Screen or window is wider than the target: pillarbox.
            float normalizedWidth = targetRatio / screenRatio;
            float barThickness = (1f - normalizedWidth) / 2f;
            _camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
        }
        else
        {
            // Screen or window is narrower than the target: letterbox.
            float normalizedHeight = screenRatio / targetRatio;
            float barThickness = (1f - normalizedHeight) / 2f;
            _camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }
    }
}
