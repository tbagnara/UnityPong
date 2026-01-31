using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    protected float speed = 5f;
    protected float boundY = 4f;

    protected int inWall = 0;

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
        vel.y = speed * SimplerMovementInput();
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

    protected float SimplerMovementInput()
    {
        if (GetMovementInput() > 0) return 1;
        else if (GetMovementInput() < 0) return -1;
        else return 0;
    }

    protected abstract float GetMovementInput();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floors"))
        {
            inWall = 1;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floors"))
        {
            inWall = 0;
        }
    }

}
