using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool pauseActive;

    public GameObject pausePanel;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pauseActive)
            {
                Time.timeScale = 1f;
                pauseActive = false;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                pauseActive = true;
                pausePanel.SetActive(pauseActive);
            }
            pausePanel.SetActive(pauseActive);
        }
    }
}
