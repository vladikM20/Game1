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
    public int ObjectsNearby
    {
        get
        {
            if (_minObjectsNearby == _maxObjectsNearby)
                return _minObjectsNearby;

            return Random.Range(_minObjectsNearby, _maxObjectsNearby + 1);
        }
    }

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
