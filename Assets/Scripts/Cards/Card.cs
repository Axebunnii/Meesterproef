using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour{
    protected string cardName;
    protected GameObject currentProjectile;

    public string CardName {
        get { return cardName; }
    }

    public Card (string name) {
        cardName = name;
    }

    public virtual void Use() {
        
    }

    protected virtual void ReplaceProjectile(GameObject prefab) {
        // Get the projectile that will be replaced
        currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
        Rigidbody2D anchor = currentProjectile.GetComponent<SpringJoint2D>().connectedBody;
        // Replace current projecile with a bomb prefab
        GameObject ins = Instantiate(prefab, currentProjectile.transform.position, Quaternion.identity);
        ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
        ins.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(0, 0);
        ins.GetComponent<SpringJoint2D>().distance = 0.005f;
        // Remove current projectile from scene
        Destroy(currentProjectile);
    }
}
