using System.Runtime.CompilerServices;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector2 velocity = new Vector2(10f,10f);
    private Rigidbody2D rb2d; 
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.linearVelocity = velocity;
        
    }

    public Vector2 Velocity
    {
        get {return velocity;}
        set {velocity = new Vector2();}
    }

    
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
