using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem.Utilities;
using Unity.Netcode;

public class BallMovement : NetworkBehaviour, ICollidable
{
    [SerializeField] private Vector2 velocity = new Vector2(10f,10f);
    private Rigidbody2D rb2d; 
    private NetworkVariable<float> xPos = new NetworkVariable<float>(0f);
    private NetworkVariable<float> yPos = new NetworkVariable<float>(0f);
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (IsServer)
        {
            xPos.Value = rb2d.position.x;
            yPos.Value = rb2d.position.y;
        }
        else if (IsClient)
        {
            rb2d.MovePosition(new Vector2(xPos.Value,yPos.Value));
        }
    }
    public Vector2 Velocity
    {
        get {return velocity;}
        set {velocity = new Vector2();}
    }    
    public void startGame()
    {
        rb2d.linearVelocity = velocity;
    }

    public void endGame()
    {
        rb2d.linearVelocity = new Vector2(0,0);
        rb2d.position = new Vector2(0,0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LeftScoreZone"))
        {
            if (IsServer) 
            {
                GameManager manager = FindFirstObjectByType<GameManager>();
                manager.addToP2();
                ballReset();
            }
        }
        else if (other.gameObject.CompareTag("RightScoreZone"))
        {
            if (IsServer) 
            {
                GameManager manager = FindFirstObjectByType<GameManager>();
                manager.addToP1();
                ballReset();
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d = GetComponent<Rigidbody2D>();

        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();
        if (collidable != null)
        {
            collidable.OnHit(collision);
            
        }

        OnHit(collision);

    }

    public void OnHit(Collision2D collision)
    {
        /*
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
        //*/

        Vector2 colNormal = collision.contacts[0].normal;

        
        if (collision.gameObject.CompareTag("Player")) 
        {
            rb2d.linearVelocity = new Vector2(-rb2d.linearVelocity.x, rb2d.linearVelocity.y);
        }
        else
        {
            rb2d.linearVelocity = Vector2.Reflect(rb2d.linearVelocity, colNormal);
        }

        

    }

    public void ballReset()
    {
        rb2d.position = new Vector2(0,0);
        rb2d.linearVelocity = new Vector2(-rb2d.linearVelocityX,-rb2d.linearVelocityY);

    }



}
