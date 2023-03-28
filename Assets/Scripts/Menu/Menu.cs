using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    [SerializeField] protected GameObject mainmenu;
    [SerializeField] protected GameObject settingsmenu;

    private void Start() {
        Screen.SetResolution(1920, 1080, true);
        settingsmenu.SetActive(false);
    }
}
