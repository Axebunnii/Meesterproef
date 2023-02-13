using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    protected GameObject projectile;
    protected SpringJoint2D springJoint;
    protected Rigidbody2D rb;
    protected Rigidbody2D anchor;

    protected bool isHold = false;
    protected float releaseTime = 0.1f;
    protected int maxDragDis = 2;
    protected float distance;

    protected int damage;

    protected void Start() {
        projectile = GameObject.Find("Projectile");
        anchor = GameObject.Find("Anchor").GetComponent<Rigidbody2D>();
        springJoint = projectile.gameObject.GetComponent<SpringJoint2D>();
        rb = projectile.gameObject.GetComponent<Rigidbody2D>();
    }

    protected void Update() {
        DragProjectile();
    }

    // Drag projectile when holding down mousebutton
    protected void OnMouseDown() {
        Debug.Log("mouse down");
        isHold = true;
        // Make it kinematic otherwise the anchor will pull it back
        rb.isKinematic = true;
    }

    // Release the projectile and remove the anchor
    protected void OnMouseUp() {
        Debug.Log("mouse up");
        isHold = false;
        rb.isKinematic = false;
        StartCoroutine(RemoveAnchor());
    }

    protected void DragProjectile() {
        if (isHold) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distance = GetDistance(mousePos);
            if (distance > maxDragDis) {
                // Add the vector pointing from the hook position to the mouse position with the lenght of the max drag distance to the anchor position
                rb.position = anchor.position + (mousePos - anchor.position).normalized * maxDragDis;
            } else {
                rb.position = mousePos;
            }
        }
    }

    protected float GetDistance(Vector2 mousePos) {
        return Vector2.Distance(mousePos, anchor.position);
    }

    protected IEnumerator RemoveAnchor() {
        yield return new WaitForSeconds(releaseTime);
        springJoint.enabled = false;
        this.enabled = false;
    }
}
