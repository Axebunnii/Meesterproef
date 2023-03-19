using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 offset = new Vector2(4, 0);
    public enum CameraFocus {player1, player2, projectile};
    public CameraFocus currentFocus;
    /*private GameObject currentFocus;
    public GameObject CurrentFocus {
        get { return currentFocus; }
        set { currentFocus = value; }
    }*/
    // Focus objects
    private GameObject focusPlayer1;
    private GameObject focusPlayer2;
    private GameObject focusProjectile;

    private void Start() {
        focusPlayer1 = GameObject.Find("Player1FocusPoint");
        focusPlayer2 = GameObject.Find("Player2FocusPoint");
        currentFocus = CameraFocus.player1;
    }

    private void Update() {
        if (currentFocus == CameraFocus.player1) { FocusOnPlayer(focusPlayer1); }
        else if (currentFocus == CameraFocus.player2) { FocusOnPlayer(focusPlayer2); }
        else if (currentFocus == CameraFocus.projectile) { FollowProjectile(); }
    }

    private void FocusOnPlayer(GameObject player) {
        this.transform.position = player.transform.position;
    }

    private void FollowProjectile() {
        focusProjectile = GameObject.FindGameObjectWithTag("Projectile");
        Vector3 position = transform.position;

        if (focusProjectile.transform.position.x > offset.x) {
            position.x = (focusProjectile.transform.position - offset).x;
            // lerp to position
            transform.position = position;
        }
        else if (focusProjectile.transform.position.x > offset.x) {
            position.x = (focusProjectile.transform.position - offset).x;
            transform.position = position;
        }
    }
}
