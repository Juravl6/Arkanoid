using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    int lives = 2;
    public int points;
    public bool invisible;
    public bool explosive;
    public float explosiveRadius;
    public GameObject pickupPrefab;
    public GameObject particlrPrefab;

    SpriteRenderer spriteRenderer;
    LevelManager levelManager;
    ScoreCounter scoreCounter;

    private void Start()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        levelManager.CreateBlock();

        if (invisible)
        {
            spriteRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (invisible)
        {
            spriteRenderer.enabled = true;
            invisible = false;
            return;
        }

        lives--;
        if (lives <= 0)
        {
            BlockDestroy();
        }
    }

    public void BlockDestroy()
    {
        scoreCounter.AddScore(points);
        levelManager.BlockDestroyed();
        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

        if (explosive)
        {
            Explode();
        }
    }

    public void Explode()
    {
        int layerMask = LayerMask.GetMask("Block");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosiveRadius, layerMask);

        foreach (Collider2D col in colliders)
        {
            //Destroy(i.gameObject);
            Block block = col.GetComponent<Block>();
            block.BlockDestroy();
            if (block == null)
            {
                Destroy(col.gameObject);
            }
            else
            {
                block.BlockDestroy();
            }
        }
        //for (int i = 0; i < colliders.Length; i++)
        //{
            //Destroy(colliders[i].gameObject;
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
