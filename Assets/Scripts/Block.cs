using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    int lives = 2;
    public int points;
    LevelManager levelManager;
    ScoreCounter scoreCounter;

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        levelManager = FindObjectOfType<LevelManager>();

        levelManager.CreateBlock();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        lives--;
        if (lives <= 0)
        {
            scoreCounter.AddScore(points);
            levelManager.BlockDestroyed();
            Destroy(gameObject);
        }
    }
}
