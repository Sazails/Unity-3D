using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets._Core.Scripts._Pathfinding
{
    public class PathFinder : MonoBehaviour
    {
        public Node[,] _grid;

        public int _gridSizeX = 6;
        public int _gridSizeY = 6;

        public Node[] _path;
        public Vector2Int _pathStart;
        public Vector2Int _pathEnd;

        public bool _showPathNeighbours = true;
        public bool _realTimePathFind = false;
        public float _realTimePathFindUpdateDelay = 1F;
        private bool _finishedUpdatingPath = true;

        private Stopwatch _pathCalcStopwatch = new Stopwatch();

        private void Start()
        {
            GenerateGrid();
            // _path = PathFind(_pathStart, _pathEnd);
        }

        private void Update()
        {
            if (_pathStart.x < 0)
                _pathStart.x = 0;
            else if (_pathStart.x > _gridSizeX)
                _pathStart.x = _gridSizeX - 1;

            if (_pathEnd.x < 0)
                _pathEnd.x = 0;
            else if (_pathEnd.x > _gridSizeX - 1)
                _pathEnd.x = _gridSizeX - 1;

            if (_pathStart.y < 0)
                _pathStart.y = 0;
            else if (_pathStart.y > _gridSizeY - 1)
                _pathStart.y = _gridSizeY - 1;

            if (_pathEnd.y < 0)
                _pathEnd.y = 0;
            else if (_pathEnd.y > _gridSizeY - 1)
                _pathEnd.y = _gridSizeY - 1;

            if (_realTimePathFind && _finishedUpdatingPath)
                StartCoroutine(GetPathFindTest());
        }

        [Button("Randomize start and end positions")]
        public void RandomizeStartAndEndPos()
        {
            _pathStart = new Vector2Int(Random.Range(0, _gridSizeX), Random.Range(0, _gridSizeY));
            _pathEnd = new Vector2Int(Random.Range(0, _gridSizeX), Random.Range(0, _gridSizeY));

            if (!_realTimePathFind && _finishedUpdatingPath)
                StartCoroutine(GetPathFindTest());
        }

        public int _iterationCount = 100;
        [Button("Test path find average time")]
        public void TestPathFindAverage()
        {
                float averageTime = 0F;
                for (int i = 0; i < _iterationCount; i++)
                {
                    _pathStart = new Vector2Int(Random.Range(0, _gridSizeX), Random.Range(0, _gridSizeY));
                    _pathEnd = new Vector2Int(Random.Range(0, _gridSizeX), Random.Range(0, _gridSizeY));
                    _pathCalcStopwatch.Restart();
                    _path = PathFind(_pathStart, _pathEnd);
                    _pathCalcStopwatch.Stop();
                    averageTime += _pathCalcStopwatch.ElapsedMilliseconds;
                }
                averageTime /= _iterationCount;
       
            Debug.LogFormat("Path calculation average. For {0} path find iterations, the average is {1}ms per path find.", _iterationCount, averageTime);
        }

        private IEnumerator GetPathFindTest()
        {
            _finishedUpdatingPath = false;
            yield return new WaitForSeconds(_realTimePathFindUpdateDelay);
            _path = PathFind(_pathStart, _pathEnd);
            _finishedUpdatingPath = true;
        }

        public void GenerateGrid()
        {
            /*
               TL    TR
                000000
                000000
                000000
                000000
                000000
                000000
               BL    BR
             */
            _grid = new Node[_gridSizeX, _gridSizeY];

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _grid[x, y]._position = new Vector2Int(x, y);
                }
            }

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    if (x == 0 && y == 0) // Grid BL
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x + 1, y); //R
                        _grid[x, y]._neighbours[1] = new Vector2Int(x + 1, y + 1); //TR
                        _grid[x, y]._neighbours[2] = new Vector2Int(x, y + 1); //T
                    }
                    else if (x == 0 && y == _gridSizeY - 1) // Grid TL
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x + 1, y); //R
                        _grid[x, y]._neighbours[1] = new Vector2Int(x + 1, y - 1); //BR
                        _grid[x, y]._neighbours[2] = new Vector2Int(x, y - 1); //B
                    }
                    else if (x == _gridSizeX - 1 && y == _gridSizeY - 1) // Grid TR
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x - 1, y); //L
                        _grid[x, y]._neighbours[1] = new Vector2Int(x - 1, y - 1); //BL
                        _grid[x, y]._neighbours[2] = new Vector2Int(x, y - 1); //B
                    }
                    else if (x == _gridSizeX - 1 && y == 0) // Grid BR
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x, y + 1); //T
                        _grid[x, y]._neighbours[1] = new Vector2Int(x - 1, y + 1); //TL
                        _grid[x, y]._neighbours[2] = new Vector2Int(x - 1, y); //L
                    }

                    else if (x == 0) // Left wall
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x, y + 1); //T
                        _grid[x, y]._neighbours[1] = new Vector2Int(x + 1, y); //R
                        _grid[x, y]._neighbours[2] = new Vector2Int(x, y - 1); //B
                    }
                    else if (y == 0) // Bottom wall
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x - 1, y); //L
                        _grid[x, y]._neighbours[1] = new Vector2Int(x, y + 1); //T
                        _grid[x, y]._neighbours[2] = new Vector2Int(x + 1, y); //R
                    }
                    else if (x == _gridSizeX - 1) // Right wall
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x, y + 1); //T
                        _grid[x, y]._neighbours[1] = new Vector2Int(x - 1, y); //L
                        _grid[x, y]._neighbours[2] = new Vector2Int(x, y - 1); //B
                    }
                    else if (y == _gridSizeY - 1) // Top wall
                    {
                        _grid[x, y]._neighbours = new Vector2Int[3];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x - 1, y); //L
                        _grid[x, y]._neighbours[1] = new Vector2Int(x, y - 1); //B
                        _grid[x, y]._neighbours[2] = new Vector2Int(x + 1, y); //R
                    }
                    else // Surrounded by 8 nodes around
                    {
                        _grid[x, y]._neighbours = new Vector2Int[8];
                        _grid[x, y]._neighbours[0] = new Vector2Int(x, y + 1); //T
                        _grid[x, y]._neighbours[1] = new Vector2Int(x + 1, y + 1); //TR
                        _grid[x, y]._neighbours[2] = new Vector2Int(x + 1, y); //R
                        _grid[x, y]._neighbours[3] = new Vector2Int(x + 1, y - 1); //BR
                        _grid[x, y]._neighbours[4] = new Vector2Int(x, y - 1); //B
                        _grid[x, y]._neighbours[5] = new Vector2Int(x - 1, y - 1); //BL
                        _grid[x, y]._neighbours[6] = new Vector2Int(x - 1, y); //L
                        _grid[x, y]._neighbours[7] = new Vector2Int(x - 1, y + 1); //TL
                    }
                }
            }
        }

        public Node[] PathFind(Vector3 start, Vector3 end)
        {
            return PathFind(new Vector2Int((int)start.x, (int)start.z), new Vector2Int((int)end.x, (int)end.z));
        }

        public Node[] PathFind(Vector2Int start, Vector2Int end)
        {
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            Node startNode = _grid[start.x, start.y];
            Node endNode = _grid[end.x, end.y];
            Node currentNode = startNode;

            currentNode._costToParent = Vector2.Distance(currentNode._position, currentNode._position);
            currentNode._costToEnd = Vector2.Distance(currentNode._position, endNode._position);
            currentNode._totalCost = currentNode._costToParent + currentNode._costToEnd;
            openList.Add(currentNode);

            while (openList.Count > 0)
            {
                // Node reached end goal, calculate the path
                if (currentNode._position == endNode._position)
                {
                    //Debug.Log("Path found to end");
                    endNode._parentPosition = currentNode._position;
                    closedList.Add(currentNode);
                    closedList.Add(endNode);
                    closedList = closedList.OrderBy(x => x._totalCost).ToList();
                    List<Node> path = new List<Node>();
                    foreach (Node n in closedList)
                    {
                        path.Add(n);

                        if (n._position == startNode._position)
                            break;
                    }
                    path.Reverse();
                    return path.ToArray();
                }

                if (closedList.Contains(currentNode))
                {
                    openList.Remove(currentNode);
                    try
                    {
                        currentNode = openList[0];
                    }
                    catch
                    {
                        currentNode = endNode;
                        //Debug.Log("No nodes left in the open list, path finding aborted.");
                        return null;
                    }
                }

                //Debug.LogFormat("Pos: {0} | Neighbour Count: {1}", currentNode._position, currentNode._neighbours.Length);
                // Reached a dead end node that isn't the end node
                if (currentNode._neighbours.Length < 2)
                {
                    currentNode._totalCost = 9999;
                    //currentNode._parentPosition = currentNode._position;
                    closedList.Add(currentNode);
                    openList.Remove(currentNode);
                    openList = openList.OrderBy(x => x._totalCost).ToList();
                    currentNode = openList[0];
                    //Debug.Log("Dead end reached, traversing back.");
                    continue;
                }

                Vector2Int lowestCostNeighbour = currentNode._neighbours[1]; ;
                for (int i = 0; i < currentNode._neighbours.Length; i++)
                {
                    Vector2Int currentNeighbourNodePos = new Vector2Int(currentNode._neighbours[i].x, currentNode._neighbours[i].y);
                    if (currentNeighbourNodePos == currentNode._position)
                        continue;

                    if (closedList.Contains(_grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]))
                    {
                        openList.Remove(_grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]);
                        try
                        {
                            currentNode = openList[0];
                        }
                        catch
                        {
                            currentNode = endNode;
                            //Debug.Log("No nodes left in the open list, path finding aborted.");
                            return null;
                        }
                    }

                    _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._costToParent = Vector2.Distance(currentNode._position, _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._position);
                    _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._costToEnd = Vector2.Distance(_grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._position, endNode._position);
                    _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._totalCost = _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._costToParent + _grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._costToEnd;

                    if (_grid[currentNeighbourNodePos.x, currentNeighbourNodePos.y]._totalCost < _grid[lowestCostNeighbour.x, lowestCostNeighbour.y]._totalCost)
                        lowestCostNeighbour = currentNeighbourNodePos;
                }

                _grid[lowestCostNeighbour.x, lowestCostNeighbour.y]._parentPosition = currentNode._position;
                openList.Add(_grid[lowestCostNeighbour.x, lowestCostNeighbour.y]);
                closedList.Add(currentNode);
                openList.Remove(currentNode);
                currentNode = _grid[lowestCostNeighbour.x, lowestCostNeighbour.y];
            }


            Debug.Log("Path not found.");
            return null;
        }

        public void OnDrawGizmosSelected()
        {
            if (_showPathNeighbours && _grid != null)
            {
                Gizmos.color = Color.yellow;
                foreach (Node node in _grid)
                {
                    foreach (Vector2 neighbour in node._neighbours)
                    {
                        Gizmos.DrawLine(new Vector3(node._position.x, 0, node._position.y), new Vector3(neighbour.x, 0, neighbour.y));
                    }
                }

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(new Vector3(_pathStart.x, 0, _pathStart.y), 0.25F);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(new Vector3(_pathEnd.x, 0, _pathEnd.y), 0.25F);
                Gizmos.color = Color.red;
                if (_path != null)
                {
                    for (int i = 0; i < _path.Length - 1; i++)
                    {
                        Vector3 s = new Vector3(_path[i]._position.x, 0, _path[i]._position.y);
                        Vector3 e = new Vector3(_path[i + 1]._position.x, 0, _path[i + 1]._position.y);
                        Gizmos.DrawLine(s, e);
                    }
                }
            }
        }
    }
}