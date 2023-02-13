using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    protected bool isHold = false;
    protected float releaseTime = 0.3f;
    protected GameObject projectile;
    protected SpringJoint2D springJoint;
    protected Rigidbody2D rb;

    protected void Start() {
        projectile = GameObject.Find("Projectile");
        springJoint = projectile.gameObject.GetComponent<SpringJoint2D>();
        rb = projectile.gameObject.GetComponent<Rigidbody2D>();
    }

    protected void Update() {
        if (isHold) rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Drag projectile when holding down mousebutton
    protected void OnMouseDown() {
        Debug.Log("mouse down");
        isHold = true;
        rb.isKinematic = true;
    }

    // Release the projectile and remove the anchor
    protected void OnMouseUp() {
        Debug.Log("mouse up");
        isHold = false;
        rb.isKinematic = false;
        StartCoroutine(RemoveAnchor());
    }

    protected IEnumerator RemoveAnchor() {
        yield return new WaitForSeconds(releaseTime);
        springJoint.enabled = false;
    }
}
