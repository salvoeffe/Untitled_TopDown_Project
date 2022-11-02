using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Animator playerAnim;

    private bool canMove = true;
    private bool isMoving = false;
    [SerializeField] float moveSpeed = 6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        float verticalSpeed = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed) * moveSpeed;

        isMoving = !(rb.velocity == Vector2.zero);

        playerAnim.SetBool("Run", isMoving);
    }
}
