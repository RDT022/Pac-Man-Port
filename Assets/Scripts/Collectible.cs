using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    public int pointValue;

    public GameManager gm;

    public abstract void OnTriggerEnter2D(Collider2D collider); 

}
