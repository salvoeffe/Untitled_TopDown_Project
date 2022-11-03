using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Animator playerAnim;

    private bool canMove = true;

    private bool moveToDesiredPos = false;
    Vector2 desiredPos;
    [SerializeField] float acceptableDistFromDesiredPos = 0.15f;

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

        if (moveToDesiredPos)
        {
            MoveToDesiredPos();
        }

        PlayAnimation();
    }

    private void Move()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal");
        float verticalSpeed = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed) * moveSpeed;
    }

    private void PlayAnimation()
    {
        isMoving = !(rb.velocity == Vector2.zero);

        playerAnim.SetBool("Run", isMoving);
    }

    public void StartMovingToDesiredPos(Vector2 pos)
    {
        canMove = false;
        moveToDesiredPos = true;
        desiredPos = pos;
    }

    private void MoveToDesiredPos()
    {
        Vector2 direction = desiredPos - (Vector2)transform.position;
        rb.velocity = direction.normalized * moveSpeed;
        if (Vector2.Distance(transform.position, desiredPos) < acceptableDistFromDesiredPos)
        {
            moveToDesiredPos = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
