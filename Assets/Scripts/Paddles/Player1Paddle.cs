using UnityEngine;

public class Player1Paddle : PaddleController
{

    protected override float GetMovementInput()
    {
        return Input.GetAxis("LeftPaddle");
    }

}
