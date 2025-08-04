using System;
using UnityEditor.EditorTools;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 5;
    public float cellSize = 1f;
    public GameObject tilePrefab;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 spawnPosition = new Vector2(x * cellSize, y * cellSize);
                GameObject tile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
                tile.name = $"Tile {x},{y}";
            }
        }
    }
}
