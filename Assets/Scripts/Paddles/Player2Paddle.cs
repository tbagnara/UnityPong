using UnityEngine;

public class Player2Paddle : PaddleController, ICollidable
{

    protected override float GetMovementInput()
    {
        return Input.GetAxis("RightPaddle");
    }

    public void OnHit(Collision2D collision)
    {
        Debug.Log("Paddle2Hit");
    }
}
