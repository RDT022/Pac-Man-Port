using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    Animator animator;

    public bool isEnabled = false;

    public int lives = 2;

    public bool poweredUp = false;

    bool canMove = true;

    public MovementDirections movementDirection = MovementDirections.Left;

    float horizMovement;

    float vertMovement;

    float momentumModifier = 7;

    public int ghostCounter = 0;

    void Start()
    {
        transform.position = new Vector3(17, -17.5f, 0);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

            if (horiz > 0 && movementDirection == MovementDirections.Left)
            {
                movementDirection = MovementDirections.Right;
            }
            else if (horiz < 0 && movementDirection == MovementDirections.Right)
            {
                movementDirection = MovementDirections.Left;
            }
            if (vert > 0 && movementDirection == MovementDirections.Down)
            {
                movementDirection = MovementDirections.Up;
            }
            else if (vert < 0 && movementDirection == MovementDirections.Up)
            {
                movementDirection = MovementDirections.Down;
            }
            ReadDirection();
            if (canMove)
            {
                rb.velocity = new Vector3(horizMovement, vertMovement, 0);
            }
            if (momentumModifier != 7)
            {
                Invoke("ResetMomentum", 0.1f);
            }
        }
    }

    void ReadDirection()
    {
        switch (movementDirection)
        {
            case MovementDirections.Left:
                animator.SetInteger("MoveDirection", 4);
                horizMovement = -momentumModifier;
                vertMovement = 0;
                break;
            case MovementDirections.Right:
                animator.SetInteger("MoveDirection", 2);
                horizMovement = momentumModifier;
                vertMovement = 0;
                break;
            case MovementDirections.Up:
                animator.SetInteger("MoveDirection", 1);
                horizMovement = 0;
                vertMovement = momentumModifier;
                break;
            default:
                animator.SetInteger("MoveDirection", 3);
                horizMovement = 0;
                vertMovement = -momentumModifier;
                break;
        }

    }

    public void SetMomentum(float f)
    {
        momentumModifier = f;
    }

    public void ResetMomentum()
    {
        momentumModifier = 7;
    }

    public void Die()
    {
        lives--;
        ghostCounter = 0;
        poweredUp = false;
        GameManager.instance.FreezeGameplay();
        animator.SetBool("Dead", true);
        StartCoroutine(DeathAnim());
    }

    IEnumerator DeathAnim()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.ResetLevel();
        animator.SetBool("Dead", false);
        animator.SetInteger("MoveDirection", 0);
    }
}
