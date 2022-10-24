using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public bool IsDead { get; private set; }

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float health = 10f;

    private Rigidbody2D rb;
    private float movementX, movementY;
    private Torch torch;
    private Torch interactableTorch = null;

    public float ProtectionRadius { get => torch.protectionRadius; }
    private float Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                IsDead = true;
            }
            GameManager.Instance.healthBar.value = health;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        torch = GetComponentInChildren<Torch>();
    }

    private void Update()
    {
        if (IsDead) return;

        movementX = Input.GetAxis("Horizontal") * movementSpeed;
        movementY = Input.GetAxis("Vertical") * movementSpeed;

        if (interactableTorch == null || !Input.GetButtonDown("Interact")) return;
        interactableTorch.Pick(torch);
        interactableTorch = null;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementX, movementY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Torch"))
        {
            interactableTorch = other.GetComponent<Torch>();
            UpdateInteractionUI(true);
        }
    }

    private void UpdateInteractionUI(bool enable)
    {
        GameManager.Instance.interactionText.gameObject.SetActive(enable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Torch"))
        {
            interactableTorch = null;
            UpdateInteractionUI(false);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
