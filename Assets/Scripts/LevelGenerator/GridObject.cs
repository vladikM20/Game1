using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _chance;
    [SerializeField] private int _minObjectsNearby;
    [SerializeField] private int _maxObjectsNearby;

    public GridLayer Layer => _layer;
    public int Chance => _chance;
    public int MinObjectsNearby => _minObjectsNearby;
    public int MaxObjectsNearby => _maxObjectsNearby;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
