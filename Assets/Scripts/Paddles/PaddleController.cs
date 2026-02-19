using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PaddleController : NetworkBehaviour
{
    protected float speed = 5f;
    protected float boundY = 4f;
    protected int inWall = 0;
    private Rigidbody2D rb2d;
    private NetworkVariable<float> yPosition = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private GameManager manager;
    

    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
        rb2d = GetComponent<Rigidbody2D>();
        startGame();
    }

    public void startGame()
    {
        if (IsOwner) {
            if (IsHost)
            {
                rb2d.transform.position = new Vector2(-8.17f, 0);
            }
            else if(IsClient) 
            {
                rb2d.transform.position = new Vector2(8.17f, 0);
            }   
        }
        else
        {
            if (IsHost)
            {
                rb2d.transform.position = new Vector2(8.17f, 0);
            }
            else if(IsClient) 
            {
                rb2d.transform.position = new Vector2(-8.17f, 0);
            }
        }
    }

    
    void Update()
    {
        if (manager.getGameOver())
        {
            startGame();
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    protected void Movement()
    {
        if (IsOwner && !manager.getGameOver() ){
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
            yPosition.Value = pos.y;
            rb2d.position = pos;
        }
        else
        {
            rb2d.position = new Vector2(transform.position.x, yPosition.Value);
        }
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
