using UnityEngine;
using System.Collections;

public abstract class ACharacter : MonoBehaviour {

    public float walkSpeed = 1;

    protected Animator animator;
    protected Rigidbody2D rigidbody;

    protected const uint STATE_IDLE = 0;

    protected float xVel;

    protected string _currentDirection = "right";
    public string CurrentDirection {
        get { return _currentDirection; }
        set {
            if (_currentDirection != value) {
                if (value == "right") {
                    transform.Rotate(0, 180, 0);
                }
                else if (value == "left") {
                    transform.Rotate(0, -180, 0);
                }

                _currentDirection = value;
            }
        }
    }

    protected uint _currentAnimationState = STATE_IDLE;
    public uint CurrentAnimationState {
        get { return _currentAnimationState; }
        set {
            animator.SetInteger("state", (int)value);
            _currentAnimationState = value;
        }
    }

    // Use this for initialization
    virtual public void Start() {
        // Each character needs an animator
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    virtual protected void Move() {
        if (CurrentDirection == "right")
            xVel = walkSpeed;
        else
            xVel = -walkSpeed;

        this.rigidbody.velocity = new Vector2(xVel, rigidbody.velocity.y);

        xVel = 0;
    }

}
