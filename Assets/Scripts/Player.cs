using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] private Slider lifePointSlider;
    [SerializeField] private Player otherPlayer;
    [SerializeField] private StateManager stateManager;
    private UIManager uiManager;

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

    private List<Card> discard = new List<Card> {};

    public List<Card> Discard {
        get { return discard; }
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
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    public void GetDamage(int damage) {
        currentLP -= damage;
        UpdateHealthBar();
        CheckCondetions();
    }

    private void UpdateHealthBar() {
        Debug.Log("Update healthbar");
        lifePointSlider.value = currentLP;
    }

    public void CheckCondetions() {
        if (currentLP <= 0 || deck.Count < 1) {
            stateManager.PlayerWon = true;
            // Show winner
            uiManager.ShowResults(otherPlayer);
        }
    }
}
