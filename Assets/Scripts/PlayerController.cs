using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10f;

    private Rigidbody2D rb;
    private float movementX, movementY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        movementY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementX, movementY);
    }
}
