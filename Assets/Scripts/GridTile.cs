using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]

public class GridTile : MonoBehaviour
{
    public GameObject towerPrefab;

    private SpriteRenderer _renderer;
    private Color _defaultColor;
    public Color hoverColor = Color.yellow;
    public Color clickedColor = Color.green;

    public int towerCost = 50;

    public bool IsOccupied { get; private set; } = false;
    public bool IsPath { get; set; } = false;

    private bool isSelected = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultColor = _renderer.color;
    }

    private void OnMouseEnter()
    {
        if (!isSelected && !IsOccupied)
        {
            _renderer.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected && !IsOccupied)
        {
            _renderer.color = _defaultColor;
        }
    }

    private void OnMouseDown()
    {
        if (IsOccupied)
        {
            Debug.Log("Tile is already occupied.");
            return;
        }

        if (!GameManager.Instance.SpendGold(towerCost))
        {
            Debug.Log("Not enough gold!");
            return;
        }

        if (towerPrefab != null)
        {
            GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            tower.transform.parent = GameObject.Find("Towers")?.transform;
            Debug.Log("Tower placed.");
        }

        isSelected = !isSelected;
        _renderer.color = isSelected ? clickedColor : _defaultColor;

        // TODO: Call GridManager or another system to handle Tower Placement.
        // For now:
        IsOccupied = isSelected;
        Debug.Log($"Tile clicked at {transform.position}");
    }
}
