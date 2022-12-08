using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionPoint : MonoBehaviour
{
    [SerializeField]
    List<string> Exits;

    public void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            string previousInput = player.movementDirection;
            player.movementDirection = ReadInputs(previousInput);
            if(string.Compare(player.movementDirection, previousInput) != 0)
            {
                collider.gameObject.transform.position = this.gameObject.transform.position;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Bonus Fruit")
        {
            BonusFruitScript fruit = collider.gameObject.GetComponent<BonusFruitScript>();
            string direction = Exits[Random.Range((int)0, (int)Exits.Count)];
            fruit.movementDirection = direction;
            collider.gameObject.transform.position = this.gameObject.transform.position;
        }
        if (collider.tag == "Ghost")
        {
            GhostScript ghost = collider.gameObject.GetComponent<GhostScript>();
            string direction = Exits[Random.Range((int)0, (int)Exits.Count)];
            ghost.movementDirection = direction;
            collider.gameObject.transform.position = this.gameObject.transform.position;
        }
    }

    public string ReadInputs(string prev)
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (horiz > 0 && Exits.Contains("right"))
        {
            return "right";
        }
        else if (horiz < 0 && Exits.Contains("left"))
        {
            return "left";
        }
        if (vert > 0 && Exits.Contains("up"))
        {
            return "up";
        }
        else if (vert < 0 && Exits.Contains("down"))
        {
            return "down";
        }
        else
        {
            return prev;
        }
    }
}
