using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletScript : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GameManager.instance.Score += pointValue;
            collider.gameObject.GetComponent<PlayerScript>().poweredUp = true;
            int index = GameManager.instance.pellets.IndexOf(this.gameObject);
            GameManager.instance.pellets.RemoveAt(index);
            Destroy(gameObject);
        }
        if (collider.tag == "Bonus Fruit")
        {
            collider.gameObject.GetComponent<BonusFruitScript>().Despawn();
            int index = GameManager.instance.pellets.IndexOf(this.gameObject);
            GameManager.instance.pellets.RemoveAt(index);
            Destroy(gameObject);
        }
    }
}
