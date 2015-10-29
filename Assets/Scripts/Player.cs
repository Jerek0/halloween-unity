using UnityEngine;
using System.Collections;
using System;

public class Player : ACharacter {

    const uint STATE_WALKING = 1;
    const uint STATE_CROUCH = 2;
    const uint STATE_JUMPING = 3;
    const uint STATE_FALLING = 4;
    const uint STATE_HURT = 5;

    bool keyUpPressed = false;
    bool keyDownPressed = false;
    bool keyRightPressed = false;
    bool keyLeftPressed = false;
    bool keySpacePressed = false;

    bool keyUpHold = false;
    bool keySpaceHold = false;

    bool _isGrounded = false;
    bool _jumpReleased = true;
    bool _isHurted = false;

    public bool IsDead {
        get { return animator.GetBool("isDead"); }
    }

    /* ######################## */
    /* ###### INITIALIZE ###### */
    /* ######################## */

    override public void Start () {
        base.Start();
        OnGroundLeave();

        GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
	}

    /* ######################## */
    /* ##### UPDATE LOOPS ##### */
    /* ######################## */

    void Update() {
        keyUpPressed = Input.GetKeyDown(KeyCode.UpArrow);        
        keyDownPressed = Input.GetKey(KeyCode.DownArrow);
        keyRightPressed = Input.GetKey(KeyCode.RightArrow);
        keyLeftPressed = Input.GetKey(KeyCode.LeftArrow);
        keySpacePressed = Input.GetKeyDown(KeyCode.Space);

        keyUpHold = Input.GetKey(KeyCode.UpArrow);
        keySpaceHold = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate() {
        // You can change where you look at any time !
        if (keyLeftPressed) CurrentDirection = "left";
        if (keyRightPressed) CurrentDirection = "right";

        if(animator.GetBool("isDead") == true) {
            // You are dead, you can't move !
        }
        else if (_isGrounded) { // We have something under our feet !

            // Reset to idle first
            CurrentAnimationState = STATE_IDLE;

            if (keyDownPressed) { // Let's CROUCH
                CurrentAnimationState = STATE_CROUCH;

                // Crouching stops movement
                StopMoving();
            }
            else if(keyUpPressed || keySpacePressed) { // Let's JUMP                
                Jump();
            }
            else { // Normal behaviour
                // Move right or left
                if (keyLeftPressed || keyRightPressed) {
                    CurrentAnimationState = STATE_WALKING;
                    Move(); 
                }
                // Stop moving
                else {
                    CurrentAnimationState = STATE_IDLE;
                    StopMoving();
                }
            }            
        } else { // We are flying !!
           
            if(rigidbody.velocity.y > 0) // Flying up
                CurrentAnimationState = STATE_JUMPING;
            else // Flying down
                CurrentAnimationState = STATE_FALLING;            

            // Boost force if still holding the jump btn
            if(!_jumpReleased) {
                JumpHigher();

                // Are we still holding the jump btn ?
                if (!keySpaceHold && !keyUpHold)
                    _jumpReleased = true;                
            }

            // If we are trying to move, move but slower than grounded
            if (keyLeftPressed || keyRightPressed) {
                MoveInTheAir();
            }
        }
    }

    /* ########################## */
    /* ##### MOVING RELATED ##### */
    /* ########################## */

    /**
     * Manages movements when player is in the air
     * Makes everything a bit slower thant Move();
     */
    void MoveInTheAir() {        
        if (CurrentDirection == "right") {
            xVel = walkSpeed * 0.1f;
            if (rigidbody.velocity.x + xVel >= walkSpeed) xVel = 0;
        }
        else {
            xVel = walkSpeed * -0.1f;
            if (rigidbody.velocity.x + xVel <= -walkSpeed) xVel = 0;
        }

        rigidbody.velocity = new Vector2(rigidbody.velocity.x + xVel, rigidbody.velocity.y);

        xVel = 0;
    }

    /**
     * Make the character descelerate a lot in order to stop him
     */
    void StopMoving() {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.8f, rigidbody.velocity.y);
    }

    /* ######################## */
    /* ##### JUMP RELATED ##### */
    /* ######################## */

    /**
     * Apply a force to make the character jump
     */
    void Jump() {
        _jumpReleased = false;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 5f);
    }

    /**
     * Boost the current vertical force to jump higher
     */
    void JumpHigher() {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 1.03f);
    }

    /* ############################# */
    /* ##### GROUNDED RELATED ###### */
    /* ############################# */

    /**
     * This is called when the character begins standing on smthg
     */
    void OnGroundTouch() {
        _jumpReleased = true;
        _isGrounded = true;
    }

    /**
     * This is called when the character isn't standing on smthg anymore
     */
    void OnGroundLeave() {
        Debug.Log("onGroundLeave");
        _isGrounded = false;
    }

    /* ######################## */
    /* ##### HURT RELATED ##### */
    /* ######################## */

    /**
     * Puts the player in a hurted state
     */
    void Hurt() {
        CurrentAnimationState = STATE_HURT;
        _isHurted = true;

        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Die", 1.5f);
    }

    void Die() {
        GetComponent<Rigidbody2D>().isKinematic = true;       
        Application.LoadLevel(Application.loadedLevel);
    }

    /**
     * Gets the player back to a normal state after being hurted
     */
    public void Heal() {
        CurrentAnimationState = STATE_IDLE;
        _isHurted = false;        
    }     
}
