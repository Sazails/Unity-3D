using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform nextPieceDisplay;
    public GameOver gameOver;
    public GamePad gamePad;
    public GameObject[] pieces;

    void Start()
    {
        SpawnDisplayPiece();
        SpawnNewPiece();
    }

    void SpawnDisplayPiece()
    {
        // Spawn display piece
        var pc = Instantiate(pieces[Random.Range(0, pieces.Length)], transform.position, Quaternion.identity);
        Piece piece = pc.GetComponent<Piece>();
        piece.enabled = false;
        piece.transform.SetParent(nextPieceDisplay);
        piece.transform.localPosition = piece.displayOffset;
        piece.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        piece.gameOver = gameOver;
    }

    public void SpawnNewPiece()
    {
        Piece pc = nextPieceDisplay.GetChild(0).GetComponent<Piece>();
        pc.transform.parent = null;
        pc.transform.position = transform.position;
        pc.transform.localScale = new Vector3(1f, 1f, 0);
        pc.enabled = true;
        gamePad.currentPiece = pc;

        SpawnDisplayPiece();
    }
}
