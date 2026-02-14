using UnityEngine;

public class Player1Paddle : PaddleController, ICollidable
{

    protected SpriteRenderer spriteR;
    protected override float GetMovementInput()
    {
        if(IsHost) {
            return Input.GetAxis("LeftPaddle");
        } else if(IsClient) {
            return Input.GetAxis("RightPaddle");
        }
        else
        {
            return 0;
        }
    }

    public void OnHit(Collision2D collision)
    {
        spriteR = GetComponent<SpriteRenderer>();
        Debug.Log("Paddle1Hit");

        if (spriteR.color != new Color(1f, 0f, 0f))
        {
            spriteR.color = new Color(1f, 0f, 0f);
        }
        else
        {
            spriteR.color = new Color(0f, 0f, 1f);
        }
        
    }

}
