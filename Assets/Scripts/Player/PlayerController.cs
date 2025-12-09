using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float moveX;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("isRun", true);
        }

        if (moveX < 0) //se o player olhar para o lado esquerdo
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("isRun", true);
        }

        if (moveX == 0)
        {
            anim.SetBool("isRun", false);

        }

        if (isGrounded == true)
        {
            addJumps = 2;
            if (Input.GetButtonDown("Jump"))
            {

                Jump();
            }
        }

        else
        {
            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
            }
        }

        Attack();



    }

    void FixedUpdate()
    {
        Move();
       
    }

    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
         anim.SetBool("isJump", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isJump", false);
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z))
        {          


            anim.Play("Attack", -1);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }





















}
