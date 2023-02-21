using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase{
    // 1. draw phase    2. card phase   3.shoot phase
    private string currentPhase = "Draw";
    public string CurrentPhase {
        get { return currentPhase; }
        set { currentPhase = value; }
    }

    private Projectile projectile = GameObject.Find("Projectile").GetComponent<Projectile>();

    public void Draw() {
        Debug.Log("draw card");
        projectile.CanShoot = false;
        // Wait till card has been drawn from the deck
        MonoInstance.instance.StartCoroutine(WaitForCardDrawn());
    }

    public void PlayCard() {
        Debug.Log("play card");
    }

    public void Shoot() {
        Debug.Log("shoot");
    }

    private IEnumerator WaitForCardDrawn() {
        while (!Input.GetKeyDown(KeyCode.Space)) {
            yield return null;
        }
        projectile.CanShoot = true;
        Debug.Log("Key pressed");
    }
}
