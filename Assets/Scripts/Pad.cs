using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    public float padMaxX;
    float yPosition;
    Pause pause;
    public bool autoplay;
    Ball ball;
    
    private void Start()
    {
        yPosition = transform.position.y;
        ball = FindObjectOfType<Ball>();
        pause = FindObjectOfType<Pause>();
    }

    private void Update()
    {
        Vector3 padNewPosition;
        if (pause.pauseActive)
        {
            return;
        }
        
        if (autoplay)
        {
            Vector3 ballPosition = ball.transform.position;
            padNewPosition = new Vector3(ballPosition.x, yPosition, 0);
        }
        else
        {
            Vector3 mousePixelPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition);
            padNewPosition = new Vector3(mouseWorldPosition.x, yPosition, 0);
   
        }

        padNewPosition.x = Mathf.Clamp(padNewPosition.x, -padMaxX, padMaxX);
        transform.position = padNewPosition;
    }
}
