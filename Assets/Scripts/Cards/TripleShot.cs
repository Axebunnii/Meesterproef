using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Card {
    public TripleShot() : base("Triple Shot") { }

    public override void Use() {
        Debug.Log("Use triple shot");
    }
}
