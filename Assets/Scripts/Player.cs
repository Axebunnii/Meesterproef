using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] private Slider lifePointSlider;

    private int currentLP;
    private int CurrentLP {
        get { return currentLP; }
    }
    private int maxLP = 1000;    

    private void Start() {
        currentLP = maxLP;
        lifePointSlider.value = currentLP;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log($"Colliding with {collision.gameObject.name}");
        Debug.Log($"Collide velocity is {collision.relativeVelocity.magnitude}");
        if (collision.gameObject.name == "Projectile" && collision.relativeVelocity.magnitude > 15) {
            Debug.Log($"Damage {this.gameObject.name}");
            GetDamage(110);
        }
    }

    private void GetDamage(int damage) {
        currentLP -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        Debug.Log("Update healthbar");
        lifePointSlider.value = currentLP;
    }
}
