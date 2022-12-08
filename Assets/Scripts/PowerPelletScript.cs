using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletScript : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            gm.Score += pointValue;
            collider.gameObject.GetComponent<PlayerScript>().poweredUp = true;
            int index = gm.pellets.IndexOf(this.gameObject);
            gm.pellets.RemoveAt(index);
            Destroy(gameObject);
        }
        if(collider.tag == "Bonus Fruit")
        {
            Destroy(collider.gameObject);
            int index = gm.pellets.IndexOf(this.gameObject);
            gm.pellets.RemoveAt(index);
            Destroy(gameObject);
        }
    }
}
