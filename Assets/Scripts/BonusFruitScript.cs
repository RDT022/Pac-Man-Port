using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFruitScript : Collectible
{
    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite baseSprite;
    [SerializeField]
    Sprite altSprite;

    bool collected = false;

    public MovementDirections movementDirection = MovementDirections.Left;

    float movementSpeed = 5;

    float horizMovement;

    float vertMovement;

    [SerializeField]
    Rigidbody2D rb;

    Vector3 despawnPos;

    void Start()
    {
        despawnPos = this.transform.position;
        this.enabled = false;
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !collected)
        {
            GameManager.instance.Score += pointValue;
            sr.sprite = altSprite;
            collected = true;
            Invoke("Despawn", 2);
        }
    }

    void Update()
    {
        ReadDirection();
        if (!collected)
        {
            rb.velocity = new Vector3(horizMovement, vertMovement, 0);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void ReadDirection()
    {
        switch (movementDirection)
        {
            case MovementDirections.Left:
                horizMovement = -movementSpeed;
                vertMovement = 0;
                break;
            case MovementDirections.Right:
                horizMovement = movementSpeed;
                vertMovement = 0;
                break;
            case MovementDirections.Up:
                horizMovement = 0;
                vertMovement = movementSpeed;
                break;
            default:
                horizMovement = 0;
                vertMovement = -movementSpeed;
                break;
        }
    }

    public void Despawn()
    {
        GameManager.instance.bonusFruitSpawned = false;
        this.enabled = false;
        this.transform.position = despawnPos;
        sr.sprite = baseSprite;
        movementDirection = MovementDirections.Left;
        collected = false;
    }
}
