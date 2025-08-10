using UnityEngine;
using UnityEngine.EventSystems;

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

    private Collider2D _collider;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _defaultColor = _renderer.color;
    }

    private void OnMouseEnter()
    {
        if (!IsOccupied) _renderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (!IsOccupied) _renderer.color = _defaultColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

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

        IsOccupied = true;
        _renderer.color = clickedColor;

        if (_collider != null) _collider.enabled = false;
    }
}