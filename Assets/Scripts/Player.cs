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
    // Deck is made of 10x TripleShot cards and 10x Bomb cards
    private List<Card> deck = new List<Card> { new Bomb(), new Bomb(), new Bomb(), new Bomb(), new Bomb(),
        new Bomb(), new Bomb(), new Bomb(),new Bomb(), new Bomb(), new TripleShot(), new TripleShot(), new TripleShot(),
        new TripleShot(), new TripleShot(), new TripleShot(), new TripleShot(), new TripleShot(), new TripleShot(), new TripleShot()};

    public List<Card> Deck {
        get { return deck; }
    }

    // Cards in the players hand
    private List<Card> hand = new List<Card>();
    public List<Card> Hand {
        get { return hand; }
        set { hand = value; }
    }

    private void Start() {
        currentLP = maxLP;
        lifePointSlider.value = currentLP;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log($"Collide velocity is {collision.relativeVelocity.magnitude}");
        if (collision.gameObject.tag == "Projectile" && collision.relativeVelocity.magnitude > 15) {
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
