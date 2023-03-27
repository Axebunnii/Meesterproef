using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectile : Projectile {

    protected override void AssignDamage() {
        damage = 100;
    }
}
