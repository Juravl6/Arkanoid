using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWalls : MonoBehaviour
{
    PlayersLife playersLife;

    private void Start()
    {
        playersLife = FindObjectOfType<PlayersLife>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ball"))
        {
            playersLife.LoseLife();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
