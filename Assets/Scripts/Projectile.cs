using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    protected GameObject projectile;
    protected Rigidbody2D rb;
    protected Rigidbody2D anchor;
    protected SpringJoint2D springJoint;
    protected CameraController mainCamera;
    protected StateManager stateManager;

    protected bool canShoot;
    public bool CanShoot {
        get { return canShoot; }
        set { canShoot = value; }
    }

    protected bool isHold = false;
    protected float releaseTime = 0.1f;
    protected int maxDragDis = 2;
    protected float distance;

    protected bool draggable = true;
    protected bool hitGround = false;
    protected float velocity;
    protected float mass;

    /*[SerializeField] protected StateManager.StateStatus state;
    public StateManager.StateStatus State {
        set { state = value; }
        get { return state; }
    }*/

    [SerializeField] protected bool assignAnchor = false;

    protected int damage;

    protected void Start() {
        assignAnchor = false;
        projectile = this.gameObject;
        rb = projectile.gameObject.GetComponent<Rigidbody2D>();
        springJoint = projectile.gameObject.GetComponent<SpringJoint2D>();
        mainCamera = Camera.main.GetComponent<CameraController>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        mass = Random.Range(0.8f, 5f);
        rb.mass = mass;
        AssignDamage();
    }

    protected void Update() {
        if (draggable && canShoot) {
            DragProjectile();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        /*// Ignore collision with another projectile
        if (collision.gameObject.tag == "Projectile") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }*/

        //if collide with ground decrease velocity
        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Border") {
            Debug.Log(collision.gameObject.name);
            velocity = collision.relativeVelocity.magnitude;
            hitGround = true;
            // Delete projectile in 3 seconds after it hit the ground
            StartCoroutine(DeleteProjectile());
        }
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.magnitude > 15) {
            collision.gameObject.GetComponent<Player>().GetDamage(damage);
            // Delete projectile in 3 seconds after it hit the player
            StartCoroutine(DeleteProjectile());
        }        
    }

    // Drag projectile when holding down mousebutton
    protected void OnMouseDown() {
       if (!canShoot) {
            return;
       }

        isHold = true;
        // Make it kinematic otherwise the anchor will pull it back
        rb.isKinematic = true;
    }

    // Release the projectile and remove the anchor
    protected void OnMouseUp() {
        if (!canShoot) {
            return;
        }

        isHold = false;
        rb.isKinematic = false;
        StartCoroutine(ReleaseProjectile());
        mainCamera.currentFocus = CameraController.CameraFocus.projectile;
        //StartCoroutine(EnableCollsion());
    }

    protected void DragProjectile() {
        if (!assignAnchor) {
            if (stateManager.State == StateManager.StateStatus.player1) anchor = GameObject.Find("AnchorP1").GetComponent<Rigidbody2D>();
            else if (stateManager.State == StateManager.StateStatus.player2) anchor = GameObject.Find("AnchorP2").GetComponent<Rigidbody2D>();
            assignAnchor = true;
        }

        if (isHold) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distance = GetDistance(mousePos);
            if (distance > maxDragDis) {
                // Add the vector pointing from the hook position to the mouse position with the lenght of the max drag distance to the anchor position
                rb.position = anchor.position + (mousePos - anchor.position).normalized * maxDragDis;
            } else {
                rb.position = mousePos;
            }

            for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) {
                if (stateManager.CurrentProjectiles[i] != gameObject) {
                    //stateManager.CurrentProjectiles[i].GetComponent<CircleCollider2D>().enabled = false;
                    stateManager.CurrentProjectiles[i].transform.position = transform.position;

                }
            }
        }
    }

    protected float GetDistance(Vector2 mousePos) {
        return Vector2.Distance(mousePos, anchor.position);
    }

    protected IEnumerator ReleaseProjectile() {
        yield return new WaitForSeconds(releaseTime);

        /*// Remove the anchor its attached to
        springJoint.enabled = false;
        draggable = false;
        //this.enabled = false;

        // Remove rotation constraints
        rb.constraints = RigidbodyConstraints2D.None;*/

        for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) {
            Projectile p = null;
            p = stateManager.CurrentProjectiles[i].GetComponent<StoneProjectile>();
            if (p != null) {
                // Remove the anchor its attached to
                p.springJoint.enabled = false;
                p.draggable = false;
                //this.enabled = false;

                // Remove rotation constraints
                p.rb.constraints = RigidbodyConstraints2D.None;
            } else {
                p = stateManager.CurrentProjectiles[i].GetComponent<BombProjectile>();
                // Remove the anchor its attached to
                p.springJoint.enabled = false;
                p.draggable = false;
                //this.enabled = false;

                // Remove rotation constraints
                p.rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    /*protected IEnumerator EnableCollsion() {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < stateManager.CurrentProjectiles.Count; i++) {
            stateManager.CurrentProjectiles[i].GetComponent<CircleCollider2D>().enabled = true;
        }
    }*/

    protected virtual IEnumerator DeleteProjectile() {
        yield return new WaitForSeconds(3);
        stateManager.CurrentProjectiles.Remove(this.gameObject);
        stateManager.CurrentState.Exit();
        Destroy(this.gameObject);
    }

    protected virtual void AssignDamage() { }
}
