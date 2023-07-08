using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var currentPos = transform.position;
        currentPos.z = 0;

        var newDirection = mousePos - currentPos;
        var perpVector = Vector3.Cross(newDirection, Vector3.back);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpVector);
    }
}
