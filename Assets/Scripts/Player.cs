using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : ACharacter {

    AudioSource audioSource;
    public List<AudioClip> audioClips;

    bool keyUpPressed = false;
    bool keyDownPressed = false;
    bool keyRightPressed = false;
    bool keyLeftPressed = false;
    bool keySpacePressed = false;    

    bool _isCrouch = false;
    public bool IsCrouch {
        set {
            _isCrouch = value;
            if(_isCrouch) IsMoving = false;
            animator.SetBool("isCrouch", value);
        }
        get { return _isCrouch; }
    }

    bool _isMoving = false;
    public bool IsMoving {
        set {
            _isMoving = value;
            animator.SetBool("isMoving", value);

            if (_isMoving) {
                if (IsGrounded) {
                    if (!audioSource.isPlaying) {
                        audioSource.Stop();
                        audioSource.loop = true;
                        audioSource.clip = audioClips[0];
                        audioSource.Play();
                    }

                    Move();
                }
                else
                    MoveInTheAir();
            }
            else audioSource.loop = false;
        }
        get { return _isMoving; }
    }

    bool _isGrounded = false;
    public bool IsGrounded {
        set {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
        get { return _isGrounded; }
    }

    bool _isDead = false;
    public bool IsDead {
        set {
            _isDead = value;
            animator.SetBool("isDead", value);
            if (_isDead) {
                audioSource.Stop();
                audioSource.PlayOneShot(audioClips[2]);

                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        get { return _isDead; }
    }

    float _velY = 0f;
    public float VelY {
        set {
            _velY = value;
            animator.SetFloat("velY", value);
        }
        get { return _velY; }
    }

    bool _jumpReleased = true;    
    

    /* ######################## */
    /* ###### INITIALIZE ###### */
    /* ######################## */

    override public void Start () {
        base.Start();
        OnGroundLeave();

        audioSource = GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));        
	}

    /* ######################## */
    /* ##### UPDATE LOOPS ##### */
    /* ######################## */

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel("Home");

        keyUpPressed = Input.GetKey(KeyCode.UpArrow);
        keySpacePressed = Input.GetKey(KeyCode.Space);

        if (IsGrounded) {
            keyDownPressed = Input.GetKey(KeyCode.DownArrow);
        } else {
            keyDownPressed = false;
        }

        keyRightPressed = Input.GetKey(KeyCode.RightArrow);
        keyLeftPressed = Input.GetKey(KeyCode.LeftArrow);

        // You can change where you look at any time !
        if (keyLeftPressed) CurrentDirection = "left";
        if (keyRightPressed) CurrentDirection = "right";
    }

    void FixedUpdate() {
        // Do smthg only if you're alive
        if(!IsDead) {

            if (keyDownPressed) {
                IsCrouch = true;
            }
            else {
                IsCrouch = false;
                
                if (keyLeftPressed || keyRightPressed) {                        
                    IsMoving = true;
                }
                else IsMoving = false;

                // JUMP
                if ((keyUpPressed || keySpacePressed) && IsGrounded && _jumpReleased) {
                    audioSource.Stop();
                    audioSource.PlayOneShot(audioClips[1]);
                    _jumpReleased = false;
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, 5f);
                } else if(!_jumpReleased && (keyUpPressed || keySpacePressed)) {
                    Debug.Log("jumphigher");
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 1.03f);
                                               
                    if (!keyUpPressed && !keySpacePressed)
                        _jumpReleased = true;
                }
            }

            // Slow down
            if(IsGrounded && !IsMoving) rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.8f, rigidbody.velocity.y);

            VelY = rigidbody.velocity.y;                              
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

    /* ############################# */
    /* ##### GROUNDED RELATED ###### */
    /* ############################# */

    /**
     * This is called when the character begins standing on smthg
     */
    void OnGroundTouch() {
        _jumpReleased = true;
        IsGrounded = true;
    }

    /**
     * This is called when the character isn't standing on smthg anymore
     */
    void OnGroundLeave() {
        IsGrounded = false;
    }

    /* ######################## */
    /* ##### HURT RELATED ##### */
    /* ######################## */
   
    public IEnumerator Hurt() {
        IsDead = true;

        yield return new WaitForSeconds(1.5f);

        GetComponent<Rigidbody2D>().isKinematic = true;
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "youWin") {
            IsDead = true;
        }
    }

    public void Collect() {
        audioSource.PlayOneShot(audioClips[3]);
    }

    public void OnEnnemyKill() {
        audioSource.PlayOneShot(audioClips[4]);
    }
    
}
