using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector3 rotationPoint;
    public Vector2 displayOffset;
    private float prevTime = 0f;
    public GameOver gameOver;

    void Start()
    {
        if (!GridBase.IsValidMove(transform))
        {
            Debug.Log("Game Over!");
            gameOver.Trigger();
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!GridBase.IsValidMove(transform))
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!GridBase.IsValidMove(transform))
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!GridBase.IsValidMove(transform))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? GridBase.fallTime / 10 : GridBase.fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!GridBase.IsValidMove(transform))
            {
                transform.position -= new Vector3(0, -1, 0);
                GridBase.AddToGrid(transform);
                GridBase.CheckForLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().SpawnNewPiece();
                Destroy(this);
            }
            prevTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.TransformPoint(rotationPoint), "Center");
    }
}
