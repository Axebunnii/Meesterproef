using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
    private GameObject handUI;
    private Player player;

    private void Start() {
        handUI = GameObject.Find("Hand");
        player = GameObject.Find("Player1").GetComponent<Player>();

        foreach (Transform handSlot in handUI.transform) {
            handSlot.gameObject.SetActive(false);
        }
        UpdateHand();
    }

    public void UpdateHand() {
        GameObject card;
        Sprite sprite;
        for (int i = 0; i < player.Hand.Count; i++) {
            card = handUI.transform.GetChild(i).gameObject;
            sprite = Resources.Load<Sprite>("Cards/Bomb");
            card.SetActive(true);
            card.GetComponent<Image>().sprite = sprite;
        }
    }

    private void ShowPlayerHand() {

    }
}
