using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour{
    protected string cardName;
    protected GameObject currentProjectile;
    protected StateManager stateManager;

    public string CardName {
        get { return cardName; }
    }

    public Card (string name) {
        cardName = name;
    }

    public virtual void Use() {
        
    }

    protected virtual void ReplaceProjectile(GameObject prefab) {
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        // Get the projectile that will be replaced
        currentProjectile = stateManager.CurrentProjectiles[0];
        for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) {
            Destroy(stateManager.CurrentProjectiles[i]);
        }
        stateManager.CurrentProjectiles.Clear();
        Rigidbody2D anchor = currentProjectile.GetComponent<SpringJoint2D>().connectedBody;
        // Replace current projecile with a bomb prefab
        GameObject ins = Instantiate(prefab, currentProjectile.transform.position, Quaternion.identity);
        ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
        ins.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(0, 0);
        ins.GetComponent<SpringJoint2D>().distance = 0.005f;
        stateManager.CurrentProjectiles.Add(ins);

        //for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) Debug.Log(stateManager.CurrentProjectiles[i]);
        // Remove current projectile from scene
        stateManager.CurrentProjectiles.Remove(currentProjectile);
    }
}
