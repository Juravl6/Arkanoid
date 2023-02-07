using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDublicate : MonoBehaviour
{
    public void ApplyEffect()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
        {
            ball.Dublicate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pad"))
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }
}

