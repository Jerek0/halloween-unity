using UnityEngine;
using System.Collections;

public class Ennemy : ACharacter {   

    const uint STATE_WALKING = STATE_IDLE;   
    const uint STATE_DIE = 1;

    // Use this for initialization
    override public void Start() {        
        base.Start();
        CurrentAnimationState = STATE_WALKING;
    }   

    void FixedUpdate() {
        Move();
    }

    override protected void Move() {
        base.Move();
    }

    void OnTriggerEnter2D(Collider2D other) {        
        if(other.name == "Player") {
            // Hurt the player
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(rigidbody.velocity.x, other.GetComponent<Rigidbody2D>().velocity.y + 2f);
            StartCoroutine(other.GetComponent<Player>().Hurt());
        }

        if(!other.isTrigger && !other.gameObject.GetComponent<PlatformEffector2D>()) {
            if (CurrentDirection == "left") {
                CurrentDirection = "right";
            }
            else {
                CurrentDirection = "left";
            }
        }        
    }

    /**
     * OnCollisionEnter2D 
     *
     * @param Collision2D coll
     */
    public void OnCollisionEnter2D(Collision2D coll) {        
        if(coll.collider.name == "Player"  && coll.relativeVelocity.y > 0f) {
            // DIE
            walkSpeed = 0;
            CurrentAnimationState = STATE_DIE;

            coll.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(coll.collider.GetComponent<Rigidbody2D>().velocity.x, 2f);

            GetComponent<BoxCollider2D>().enabled = false;
        }          
    }
}
