using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    private string cardName;
    public string CardName {
        get { return cardName; }
    }

    public Card (string name) {
        cardName = name;
    }
}
