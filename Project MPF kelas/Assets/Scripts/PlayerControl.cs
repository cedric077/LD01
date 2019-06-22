using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;

    private float moveSpeedStore;
    private float speedMilestoneCountStore;
    private float speedIncreaseMilestoneStore;

    public float jumpForce;

    public float jumpTime;
    private float jumpTimeCounter;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    public AudioSource jump;
    public AudioSource death;
    //private Collider2D myCollider;

    private Rigidbody2D myRigidBody;

    private Animator myAnimator;

    public GameManager theGameManager;

    private bool canDoubleJump;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
	}
	
	// Update is called once per frame
	void Update () {

        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump.Play();
        }
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jump.Play();
            }

            if (!grounded && canDoubleJump)
            {
                jumpTimeCounter = jumpTime;
                canDoubleJump = false;
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jump.Play();
            }

        }

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if(jumpTimeCounter > 0)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

     

        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

	}

    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "killzone")
        {
            
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            death.Play();
        }
    }
}
