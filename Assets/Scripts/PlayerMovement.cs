using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private float dirX = 0f;
    private BoxCollider coll;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        dirX = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


    }
}
