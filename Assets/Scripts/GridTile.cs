using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridTile : MonoBehaviour
{
    [Header("Colors")]
    public Color hoverColor = Color.yellow;
    public Color clickedColor = Color.green;

    [Header("Flags")]
    public bool IsPath = false;

    private SpriteRenderer _renderer;
    private Color _defaultColor;
    private BuildSpot _spot;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultColor = _renderer.color;
        _spot = GetComponent<BuildSpot>();
    }

    void OnMouseEnter()
    {
        if (IsPath) return;
        if (_spot != null && _spot.occupied) return;

        _renderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        if (_spot != null && _spot.occupied)
        {
            _renderer.color = clickedColor;
            return;
        }
        _renderer.color = _defaultColor;
    }

    public void SetOccupiedVisual(bool occupied)
    {
        _renderer.color = occupied ? clickedColor : _defaultColor;
    }
}