using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScript : Collectible
{
    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite altSprite;

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            gm.Score += pointValue;
            if(this.tag == "Fat Pellet")
            {
                collider.gameObject.GetComponent<PlayerScript>().SetMomentum(2.5f);
            }
            int index = gm.pellets.IndexOf(this.gameObject);
            gm.pellets.RemoveAt(index);
            Destroy(gameObject);
        }
        if(collider.tag == "Bonus Fruit")
        {
            pointValue = 50;
            sr.sprite = altSprite;
            this.tag = "Fat Pellet";
        }
    }
}
