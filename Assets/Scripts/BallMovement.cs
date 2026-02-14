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



}
