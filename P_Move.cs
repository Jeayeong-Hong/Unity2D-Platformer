using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Move : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpPower;  // jumpPower 값을 더 크게 설정하여 점프 속도를 높입니다.
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private bool isGrounded;
    //
    private int attackIndex = 0;
    private float lastAttackTime;
    public float comboDelay = 0.7f;
    private bool isAttacking = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            isGrounded = false;
        }

        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (3f - 1) * Time.deltaTime;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0)
        {
            spriteRenderer.flipX = horizontalInput < 0;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Check if grounded
        if (isGrounded && rigid.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.time - lastAttackTime > comboDelay)
            {
                attackIndex = 1;
            }
            else
            {
                attackIndex++;
                if (attackIndex > 3) attackIndex = 1;
            }

            lastAttackTime = Time.time;
            anim.SetInteger("attackIndex", attackIndex);
            anim.SetTrigger("doAttack");
            isAttacking = true;
        }
        //Death 애니메이션 (C 키)
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("doDeath");
        }

        // Take Hit 애니메이션 (X 키)
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("doHit");
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(horizontalInput * maxSpeed, rigid.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if there are any contacts
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            if (rigid.velocity.y == 0)
            {
                anim.SetBool("isJumping", false);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if there are any contacts
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = false;
        }
    }
    // 애니메이션 이벤트 함수
    public void EndAttack()
    {
        anim.SetInteger("attackIndex", 0);
        anim.ResetTrigger("doAttack");
        isAttacking = false;
    }
}