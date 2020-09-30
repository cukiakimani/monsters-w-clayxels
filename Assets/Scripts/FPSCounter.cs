using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int targetFramerate = -1;

    private const float refreshTime = 0.1f;

    private int frameCounter;
    private float timer;
    private float framerate;
    private GUIStyle labelStyle;

    private void Awake()
    {
        Application.targetFrameRate = targetFramerate;
        labelStyle = new GUIStyle {fontSize = 18, fontStyle = FontStyle.Bold};
    }

    private void Update()
    {
        if (timer < refreshTime)
        {
            timer += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            framerate = frameCounter / timer;
            frameCounter = 0;
            timer = 0f;
        }
    }

    private void OnGUI()
    {
        Color textColor = Color.green;

        if (framerate < 45f)
            textColor = Color.yellow;

        if (framerate < 30f)
            textColor = Color.red;

        GUI.Box(new Rect(8, 8, 50, 25), "");
        
        labelStyle.normal.textColor = Color.black;
        GUI.Label(new Rect(11.5f, 11.5f, 50, 25), framerate.ToString("n1"), labelStyle);

        labelStyle.normal.textColor = textColor;
        GUI.Label(new Rect(10, 10, 50, 25), framerate.ToString("n1"), labelStyle);

    }
}