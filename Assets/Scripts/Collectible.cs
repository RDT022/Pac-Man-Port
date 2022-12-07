using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Collectible : MonoBehaviour
{
    [SerializeField]
    int pointValue;

    [SerializeField]
    GameManager gm;

    public abstract void OnTriggerEnter2D(Collider2D collider); 

}
