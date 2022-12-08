using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFruitScript : Collectible
{
    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite altSprite;

    bool collected = false;

    public string movementDirection = "left";

    float horizMovement;

    float vertMovement;

    [SerializeField]
    Rigidbody2D rb;

    void Start()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            gm.Score += pointValue;
            sr.sprite = altSprite;
            collected = true;
            gm.bonusFruitSpawned = false;
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
        if (movementDirection == "left")
        {
            horizMovement = -5;
            vertMovement = 0;
        }
        if (movementDirection == "right")
        {
            horizMovement = 5;
            vertMovement = 0;
        }
        if (movementDirection == "up")
        {
            horizMovement = 0;
            vertMovement = 5;
        }
        if (movementDirection == "down")
        {
            horizMovement = 0;
            vertMovement = -5;
        }
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}
