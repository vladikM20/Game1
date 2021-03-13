using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetX;

    private void Update()
    {
        Vector3 target = new Vector3(_target.position.x + _offsetX, transform.position.y, transform.position.z);

        transform.position = target;
    }
}
