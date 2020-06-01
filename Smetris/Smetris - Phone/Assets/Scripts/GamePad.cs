using UnityEngine;

public class GamePad : MonoBehaviour
{
    public Piece currentPiece;

    public void MoveLeft()
    {
        currentPiece.MoveLeft();
    }

    public void MoveRight()
    {
        currentPiece.MoveRight();
    }

    public void MoveDown()
    {
        currentPiece.MoveDown();
    }

    public void Rotate()
    {
        currentPiece.Rotate();
    }
}
