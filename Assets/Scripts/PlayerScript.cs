using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    public bool poweredUp = false;

    bool canMove = true;

    public string movementDirection = "left";

    float horizMovement;

    float vertMovement;

    float momentumModifier = 7;

    public int ghostCounter = 0;

    [SerializeField]
    GameManager gm;

    void Start()
    {
        transform.position = new Vector3(17, -17.5f , 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (horiz > 0 && movementDirection == "left")
        {
            movementDirection = "right";
        }
        else if (horiz < 0 && movementDirection == "right")
        {
            movementDirection = "left";
        }
        if (vert > 0 && movementDirection == "down")
        {
            movementDirection = "up";
        }
        else if (vert < 0 && movementDirection == "up")
        {
            movementDirection = "down";
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

    void ReadDirection()
    {
        if(movementDirection == "left")
        {
            horizMovement = -momentumModifier;
            vertMovement = 0;
        }
        if(movementDirection == "right")
        { 
            horizMovement = momentumModifier;
            vertMovement = 0;
        }
        if (movementDirection == "up")
        {
            horizMovement = 0;
            vertMovement = momentumModifier;
        }
        if (movementDirection == "down")
        {
            horizMovement = 0;
            vertMovement = -momentumModifier;
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
        gm.LoadScene("Scene 1");
    }
}
