using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]

public class GridTile : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Color _defaultColor;
    public Color hoverColor = Color.yellow;
    public Color clickedColor = Color.green;

    private bool isSelected = false;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultColor = _renderer.color;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            _renderer.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            _renderer.color = _defaultColor;
        }
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;
        _renderer.color = isSelected ? clickedColor : _defaultColor;

        // TODO: Call GrindManager or another system to handle Tower Placement.
        Debug.Log($"Tile clicked at {transform.position}");
    }
}
