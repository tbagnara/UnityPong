using UnityEngine;

public class Player2Paddle : PaddleController
{

    protected override float GetMovementInput()
    {
        return Input.GetAxis("RightPaddle");
    }

}
