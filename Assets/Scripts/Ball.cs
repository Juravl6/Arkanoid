using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;

    float speedKoef;
    public float explosiveRadius;
    public float speed;
    bool explosive;
    bool isStarted;
    bool isMagnetActive;
    float randomX;
    float xDelta;
    public GameObject explosiveEffect;
    public AudioClip audioClip;
    AudioSource audioSource;

    public Pad pad;

    float yBallPosition;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        yBallPosition = transform.position.y;
        xDelta = transform.position.x - pad.transform.position.x;
    }

    public void Dublicate()
    {
        Ball originalBall = this;
        Ball newBall = Instantiate(originalBall);
        newBall.speed = speed;
        if (isMagnetActive)
        {
            newBall.isMagnetActive = true;
        }
    }

    public void Magnet()
    {
        isMagnetActive = true;
    }

    public void MultiplySpeed(float speedKoef)
    {
        speed *= speedKoef;
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void isExplosive()
    {
        explosive = true;
        explosiveEffect.SetActive(true);
        audioSource.PlayOneShot(audioClip);
    }

    public void RestartBall()
    {
        isStarted = false;
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();

        if(isMagnetActive && collision.gameObject.CompareTag("pad"))
        {
            Start();
            RestartBall();
        }

        if (explosive && collision.gameObject.CompareTag("Block"))
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
        }
    }
    private void Update()
    {
        if (isStarted)
        {

        }
        else
        {
            randomX = Random.Range(-5f, 5f);
            Vector3 ballNewPosition = new Vector3(pad.transform.position.x, yBallPosition, 0);
            transform.position = ballNewPosition;

            if (Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }
        print(rb.velocity.magnitude);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, rb.velocity);
    }

    private void StartBall()
    {
        randomX = Random.Range(-5f, 5f);
        Vector2 direction = new Vector2(randomX, 1);
        Vector2 force = direction.normalized * speed;
        //rb.AddForce(force);
        rb.velocity = force;
        isStarted = true;
    }
}
