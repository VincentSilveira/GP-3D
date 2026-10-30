
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class PathfindingManager : MonoBehaviour
{
    public Vector2 gridOrigin = new Vector2(-10, -6);
    public int gridWidth = 20;
    public int gridHeight = 12;
    public float nodeSize = 1f;
    public LayerMask obstacleLayer;

    private Pathfinder<Vector2Int> pathfinder;

    void Awake()
    {
        pathfinder = new Pathfinder<Vector2Int>(
            HeuristicDistance,
            ConnectedNodesAndStepCosts,
            9999
        );
    }

    float HeuristicDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    Dictionary<Vector2Int, float> ConnectedNodesAndStepCosts(
        Vector2Int cell)
    {
        Dictionary<Vector2Int, float> neighbours =
            new Dictionary<Vector2Int, float>();

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int next = cell + direction;

            if (next.x < 0 || next.x >= gridWidth ||
                next.y < 0 || next.y >= gridHeight)
                continue;

            Vector2 worldPosition = CellToWorld(next);

            bool blocked = Physics2D.OverlapBox(
                worldPosition,
                Vector2.one * nodeSize * 0.8f,
                0f,
                obstacleLayer
            );

            if (!blocked)
                neighbours[next] = 1f;
        }

        return neighbours;
    }

    Vector2Int WorldToCell(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(
                (position.x - gridOrigin.x) / nodeSize),
            Mathf.FloorToInt(
                (position.y - gridOrigin.y) / nodeSize)
        );
    }

    Vector2 CellToWorld(Vector2Int cell)
    {
        return gridOrigin + new Vector2(
            (cell.x + 0.5f) * nodeSize,
            (cell.y + 0.5f) * nodeSize
        );
    }

    public bool FindWorldPath(
        Vector2 start,
        Vector2 target,
        out List<Vector2> worldPath)
    {
        worldPath = new List<Vector2>();

        List<Vector2Int> cellPath;

        bool found = pathfinder.GenerateAstarPath(
            WorldToCell(start),
            WorldToCell(target),
            out cellPath
        );

        if (!found)
            return false;

        foreach (Vector2Int cell in cellPath)
            worldPath.Add(CellToWorld(cell));

        return true;
    }
}
