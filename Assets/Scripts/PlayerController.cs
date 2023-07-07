using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        var moveDirection = ProcessInputs(); 
        moveDirection *= (speed * Time.deltaTime);
        
        transform.Translate(moveDirection.x, moveDirection.y, 0);
    }

    private Vector2 ProcessInputs() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
}
