using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpeed : MonoBehaviour
{
    public float speedKoef;

    public void ApplyEffect()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach(Ball ball in balls)
        {
            ball.MultiplySpeed(speedKoef);
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
