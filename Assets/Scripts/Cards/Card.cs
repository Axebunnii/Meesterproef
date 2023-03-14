using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    protected string cardName;
    public string CardName {
        get { return cardName; }
    }

    public Card (string name) {
        cardName = name;
    }

    public virtual void Use() {
        Debug.Log("Use card");
    }
}
