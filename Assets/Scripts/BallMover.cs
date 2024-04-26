using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMover : MonoBehaviour
{

    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedIncreament = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;

    private int hitCounter;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("KickBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncreament * hitCounter));
    }

    private void KickBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncreament * hitCounter);
    }

    private void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("KickBall", 2f);
    }

    private void Bounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = myObject.position;

        float xDirection;
        float yDirection;

        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        yDirection = (ballPosition.y - playerPosition.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncreament * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerPaddle" || collision.gameObject.name == "AIPaddle")
        {
            Bounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            Reset();
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }
        else if(transform.position.x  < 0)
        {
            Reset();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();    
        }
    }


}
