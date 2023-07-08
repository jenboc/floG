using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _mousePos;
    
    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _mousePos.z = 0;
        var currentPos = transform.position;
        currentPos.z = 0;

        var newDirection = _mousePos - currentPos;
        var perpVector = Vector3.Cross(newDirection, Vector3.back);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpVector);
    }

    public void Fire()
    {
        var fireFrom = transform.TransformPoint(0, 0, 0);
        fireFrom.z = 0;
        var fireDir = _mousePos - fireFrom;
        fireDir.z = 0;

        var hit = Physics2D.Raycast(fireFrom, fireDir, float.PositiveInfinity);

        if (!hit)
            return;

        var obj = hit.transform;

        if (!obj.CompareTag("Ball"))
            return;

        obj.GetComponent<BallController>().Shoot(); 
    }
}
