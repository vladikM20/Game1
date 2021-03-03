using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private Transform _ball;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _cellSize;

    private HashSet<Vector2Int> _collisionsMatrix = new HashSet<Vector2Int>();

    private void Update()
    {
        FillRadius(_ball.position, _viewRadius);
    }

    private void FillRadius(Vector2 center, float viewRadius)
    {
        int cellCountOnAxis = (int)(viewRadius / _cellSize);
        Vector2Int fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            TryCreateObjectOnLayer(GridLayer.Ground, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateObjectOnLayer(GridLayer.OnGround, fillAreaCenter + new Vector2Int(x, 0));
            TryCreateObjectOnLayer(GridLayer.InAir, fillAreaCenter + new Vector2Int(x, 0));
        }
    }

    private void TryCreateObjectOnLayer(GridLayer layer, Vector2Int gridPosition)
    {
        gridPosition.y = (int)layer;

        GridObject template = GetRandomTemplate(layer);
        int objectsNearby = GetRandomObjectsNearbyCount(template);

        for (int i = 0; i < objectsNearby; i++)
        {
            if (TryAddPositionToMatrix(gridPosition + new Vector2Int(i, 0)) == false)
                return;
        }

        if (template.Chance < Random.Range(0, 100))
            return;

        for (int i = 0; i < objectsNearby; i++)
        {
            Vector2 position = GridToWorldPosition(gridPosition) + (Vector2)transform.position;
            gridPosition.x++;

            Instantiate(template, position, Quaternion.identity, transform);
        }
    }

    private bool TryAddPositionToMatrix(Vector2Int gridPosition)
    {
        if (_collisionsMatrix.Contains(gridPosition))
            return false;
        else
            _collisionsMatrix.Add(gridPosition);

        return true;
    }

    private GridObject GetRandomTemplate(GridLayer layer)
    {
        var variants = _templates.Where(template => template.Layer == layer);
        int randomIndex = Random.Range(0, variants.Count());

        return variants.ElementAt(randomIndex);
    }

    private int GetRandomObjectsNearbyCount(GridObject template)
    {
        if (template.MinObjectsNearby == template.MaxObjectsNearby)
            return template.MinObjectsNearby;

        return Random.Range(template.MinObjectsNearby, template.MaxObjectsNearby + 1);
    }

    private Vector2 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector2(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize);
    }

    private Vector2Int WorldToGridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize));
    }
}
