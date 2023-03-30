using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;
using Random = UnityEngine.Random;

public class IntersectionPoint : MonoBehaviour
{
    [SerializeField]
    List<MovementDirections> Exits;

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerScript player = collider.gameObject.GetComponent<PlayerScript>();
            MovementDirections previousInput = player.movementDirection;
            player.movementDirection = ReadInputs(previousInput);
            if (player.movementDirection != previousInput)
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
            MovementDirections direction = Exits[Random.Range((int)0, (int)Exits.Count)];
            if (fruit.movementDirection != direction)
            {
                fruit.movementDirection = direction;
                collider.gameObject.transform.position = this.gameObject.transform.position;
            }
        }
        if (collider.tag == "Ghost")
        {
            GhostScript ghost = collider.gameObject.GetComponent<GhostScript>();
            MovementDirections direction = Exits[Random.Range((int)0, (int)Exits.Count)];
            if (ghost.movementDirection != direction)
            {
                ghost.movementDirection = direction;
                collider.gameObject.transform.position = this.gameObject.transform.position;
            }
        }
    }

    public MovementDirections ReadInputs(MovementDirections prev)
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (horiz > 0 && Exits.Contains(MovementDirections.Right))
        {
            return MovementDirections.Right;
        }
        else if (horiz < 0 && Exits.Contains(MovementDirections.Left))
        {
            return MovementDirections.Left;
        }
        if (vert > 0 && Exits.Contains(MovementDirections.Up))
        {
            return MovementDirections.Up;
        }
        else if (vert < 0 && Exits.Contains(MovementDirections.Down))
        {
            return MovementDirections.Down;
        }
        else
        {
            return prev;
        }
    }
}
