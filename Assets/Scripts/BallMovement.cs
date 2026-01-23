using System.Runtime.CompilerServices;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float forceX = 10f;
    public float forceY = 10f;
    private Rigidbody2D rb2d; 
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.linearVelocity = new Vector2(forceX, forceY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (collision.gameObject.CompareTag("Floors"))
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, -rb2d.linearVelocity.y);
            Debug.Log("Floor");
        }
        else
        {
            rb2d.linearVelocity = new Vector2(-rb2d.linearVelocity.x, rb2d.linearVelocity.y);
            Debug.Log("Wall");
        }


    }

}
