using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Card {
    public Bomb() : base("Bomb") {}

    public override void Use() {
        GameObject prefab = Resources.Load("Projectiles/Bomb") as GameObject;
        ReplaceProjectile(prefab);
    }
}
