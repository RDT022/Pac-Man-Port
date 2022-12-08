using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    bool eaten;

    public bool vulnerable;

    public string movementDirection = "left";

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
    Sprite eatableSprite;

    [SerializeField]
    Sprite baseSprite;

    [SerializeField]
    GameManager gm;


    void Start()
    {
        startingPos = this.gameObject.transform.position;
    }

    void Update()
    {
        ReadDirection();
        if (!eaten)
        {
            if(!vulnerable)
            {
                sr.sprite = baseSprite;
            }
            else
            {
                sr.sprite = eatableSprite;
            }
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            if (!vulnerable && !eaten)
            {
                player.Die();
            }
            else
            {
                eaten = true;
                gm.Score += pointValues[player.ghostCounter];
                sr.sprite = pointSprites[player.ghostCounter];
                player.ghostCounter++;
            }
        }
    }

    void Reset()
    {
        this.gameObject.transform.position = startingPos;
        eaten = false;
        vulnerable = false;
    }
}
