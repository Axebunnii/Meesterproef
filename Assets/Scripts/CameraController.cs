using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 offset = new Vector2(4, 0);
    private GameObject cameraFocus;
    public GameObject CameraFocus {
        get { return cameraFocus; }
        set { cameraFocus = value; }
    }
    [SerializeField] private GameObject playerOneFocus;

    private void Start() {
        cameraFocus = playerOneFocus;
    }

    private void Update() {
        Vector3 position = transform.position;
        if (cameraFocus.transform.position.x > 4) {
            position.x = (cameraFocus.transform.position - offset).x;
            transform.position = position;
        } else if (cameraFocus.transform.position.x > 4) {
            position.x = (cameraFocus.transform.position - offset).x;
            transform.position = position;
        }
    }

    private void SetFocusPoint() {

    }
}
