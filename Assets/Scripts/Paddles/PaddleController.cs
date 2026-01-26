using UnityEngine;

public class PaddleController : MonoBehaviour
{
    protected float speed = 5f;
    protected float boundY = 5f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var vel = rb2d.linearVelocity;
        vel.y = speed * GetMovementInput();
        rb2d.linearVelocity = vel;

        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;

    }

    protected virtual float GetMovementInput()
    {
        return 0f;
    }

}
