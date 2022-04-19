using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameLogic logic;

    public GameObject startFlag;

    //Movement
    public int playerSpeed = 6;

    public float jumpHeight = 14;

    public Transform groundCheck;

    public float checkRadius;

    public float moveX;

    private bool facingRight = true;

    public LayerMask whatIsGround;

    private Rigidbody2D _rigidbody;

    public bool isGrounded;

    public int dir;

    public bool jumpPressed;

    //animation
    public Animator animator;

    void Start()
    {
        // leftText.text = keys["left"].ToString().ToLower();
        // rightText.text = keys["right"].ToString().ToLower();
        // jumpText.text = keys["jump"].ToString().ToLower();
        // switchText.text = keys["switch"].ToString().ToLower();
        gameObject.transform.position =  new Vector3(startFlag.transform.position.x, startFlag.transform.position.y + 1, 0);
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();

        animator.SetBool("Jump", false);
    }

    void Update()
    {
        dir = logic.leftPressed() && !logic.rightPressed() ? -1 : logic.rightPressed() && !logic.leftPressed() ? 1 : 0;
        jumpPressed = logic.jumpPressed();
        Animate();

        if(Input.GetKeyDown(KeyCode.L))
        {
            kill();
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //moveX check
        moveX = dir;

        //jump

        if (isGrounded && jumpPressed && _rigidbody.velocity.y == 0)
        {
            _rigidbody.AddForce(new Vector3(0, jumpHeight), ForceMode2D.Impulse);

        }

        if (!jumpPressed && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * .5f);
        }

        //playerdirection
        if (moveX > 0.0f && facingRight == false || moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        //physics
        _rigidbody.velocity = new Vector2(moveX * playerSpeed, _rigidbody.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //animation

    }
    void FlipPlayer()
    {
        //flipcode
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Animate()
    {
        if (!Mathf.Approximately(_rigidbody.velocity.x, 0))
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", -1); // should be -1
        }

        if (Mathf.Approximately(_rigidbody.velocity.y, 0))
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
        else if (_rigidbody.velocity.y > 0)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }
        else if (_rigidbody.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
    }

    public void kill()
    {
        gameObject.transform.position =  new Vector3(startFlag.transform.position.x, startFlag.transform.position.y + 1, 0);
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8)
        {
            kill();
        }
    }
}