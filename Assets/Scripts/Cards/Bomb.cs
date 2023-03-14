using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Card {
    public Bomb() : base("Bomb") { }

    public override void Use() {
        Debug.Log("Use bomb");
    }
}
