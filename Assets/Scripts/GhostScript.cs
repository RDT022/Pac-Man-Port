using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GhostScript : MonoBehaviour
{
    bool eaten;

    public bool isEnabled = false;

    public bool vulnerable;

    public MovementDirections movementDirection = MovementDirections.Left;

    float movementSpeed = 5;

    float horizMovement;

    float vertMovement;

    [SerializeField]
    Rigidbody2D rb;

    Vector3 startingPos;

    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite[] pointSprites;

    [SerializeField]
    int[] pointValues;

    [SerializeField]
    AnimatorController RegularAnims;

    [SerializeField]
    AnimatorController VulnerableAnims;

    public Animator animator;

    void Start()
    {
        startingPos = this.gameObject.transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (isEnabled && !eaten)
        {
            if (!vulnerable)
            {
                animator.runtimeAnimatorController = RegularAnims;
            }
            else
            {
                animator.runtimeAnimatorController = VulnerableAnims;
            }
            ReadDirection();
            rb.velocity = new Vector3(horizMovement, vertMovement, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
            Invoke("Reset", 2);
        }
    }

    void ReadDirection()
    {
        switch (movementDirection)
        {
            case MovementDirections.Left:
                horizMovement = -movementSpeed;
                vertMovement = 0;
                animator.SetInteger("MovementDirection", 4);
                break;
            case MovementDirections.Right:
                horizMovement = movementSpeed;
                vertMovement = 0;
                animator.SetInteger("MovementDirection", 2);
                break;
            case MovementDirections.Up:
                horizMovement = 0;
                vertMovement = movementSpeed;
                animator.SetInteger("MovementDirection", 1);
                break;
            default:
                horizMovement = 0;
                vertMovement = -movementSpeed;
                animator.SetInteger("MovementDirection", 3);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            if (!vulnerable && !eaten)
            {
                player.Die();
            }
            else if (!eaten)
            {
                eaten = true;
                GameManager.instance.Score += pointValues[player.ghostCounter];
                animator.runtimeAnimatorController = null;
                sr.sprite = pointSprites[player.ghostCounter];
                player.ghostCounter++;
            }
        }
    }

    public void turnTransparent()
    {
        sr.color = new Color(1, 1, 1, 0);
    }

    public void Reset()
    {
        this.gameObject.transform.position = startingPos;
        eaten = false;
        vulnerable = false;
        sr.color = new Color(1, 1, 1, 1);
    }

    public void amplifySpeed(int increase)
    {
        movementSpeed += increase;
    }
}
