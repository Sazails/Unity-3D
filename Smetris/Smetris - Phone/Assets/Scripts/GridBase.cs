using UnityEngine;

public class GridBase : MonoBehaviour
{
    public static int width = 16;
    public static int height = 28;
    public static float fallTime = 0.6f;
    public static Transform[,] grid = new Transform[width, height];

    public static ScoreBase scoreBase;

    void Start()
    {
        scoreBase = FindObjectOfType<ScoreBase>();
    }

    public static void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    public static bool HasLine(int r)
    {
        for (int l = 0; l < width; l++)
        {
            if (grid[l, r] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static void DeleteLine(int r)
    {
        for (int l = 0; l < width; l++)
        {
            Destroy(grid[l, r].gameObject);
            grid[l, r] = null;
        }
        scoreBase.AddScore(50 * width);
    }

    public static void RowDown(int r)
    {
        for (int y = r; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public static void AddToGrid(Transform t)
    {
        foreach (Transform child in t)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            grid[x, y] = child;
        }
    }

    public static bool IsValidMove(Transform t)
    {
        foreach (Transform child in t)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return false;
            }

            if (grid[x, y] != null)
            {
                return false;
            }
        }

        return true;
    }
}
