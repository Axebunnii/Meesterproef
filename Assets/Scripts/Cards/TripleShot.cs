using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Card {
    public TripleShot() : base("Triple Shot") {}

    public override void Use() {
        GameObject prefab = Resources.Load("Projectiles/Stone") as GameObject;
        ReplaceProjectile(prefab);
    }

    protected override void ReplaceProjectile(GameObject prefab) {
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        GameObject ins;
        // Get the projectile that will be replaced
        currentProjectile = GameObject.FindGameObjectWithTag("Projectile");
        Rigidbody2D anchor = currentProjectile.GetComponent<SpringJoint2D>().connectedBody;
        // Replace current projecile with a three stone prefabs
        for (int i = 0; i < 3; i++) {
            Debug.Log("Spawn projectile");
            ins = Instantiate(prefab, currentProjectile.transform.position, Quaternion.identity);
            ins.GetComponent<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
            ins.GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(0, 0);
            ins.GetComponent<SpringJoint2D>().distance = 0.005f;
            ins.GetComponent<StoneProjectile>().CanShoot = true;
            stateManager.CurrentProjectiles.Add(ins);
        }

        // Remove current projectile from scene
        stateManager.CurrentProjectiles.Remove(currentProjectile);
        Destroy(currentProjectile);
    }
}
