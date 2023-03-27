using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombProjectile : Projectile {
    private ParticleSystem explosion;

    protected override IEnumerator DeleteProjectile() {
        explosion = gameObject.GetComponentInChildren<ParticleSystem>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("None");
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        explosion.Play();
        yield return new WaitForSeconds(1f);

        stateManager.CurrentState.Exit();
        Destroy(this.gameObject);
    }

    protected override void AssignDamage() {
        damage = 250;
    }
}
