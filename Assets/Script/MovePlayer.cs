using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour

{


    [SerializeField]
    private Rigidbody2D rig2d;
    public AudioClip flySound;
    public AudioSource soundSource;
    public Animator anim;
    public SpriteRenderer m_sprite;
    public SpriteRenderer m_balloon;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 startPos;
    public Vector2 direction;

    public float speed = 2f;
    public float distToGround;
    public float bounceSpeed = 2f;

    public float groundCheckRadius = 2f; //REMOVER APÓS FINALIZAR TESTES

    public Text m_Text;


    float elapsedTime;
    float flyInterval = 0.3f;
    bool isFlying;
    bool isGrounded;
    bool moveRight;
    bool moveLeft;
    string message;

    private void Update()
    {

        // VERIFICA SE A PLATAFORMA É IOS

#if UNITY_IOS

        if (moveRight)
        {
            MoveRight();
        }
        if (moveLeft)
        {
            MoveLeft();
        }

#endif

        elapsedTime += Time.deltaTime;


        // isFlying = Input.GetMouseButton(0);

        // Debug.Log(isFlying);

        // Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        // transform.position = transform.position + horizontal * Time.deltaTime * speed;

        // if (horizontal.x < 0f)
        // {
        //     moveLeft = true;
        //     transform.localScale = new Vector3(-2.3f, 2.3f, 2.3f);
        //     if (!isGrounded)
        //     {
        //         transform.rotation = Quaternion.AngleAxis(-14f, Vector3.back);
        //     }
        // }
        // if (horizontal.x > 0f)
        // {
        //     moveRight = true;
        //     transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        //     if (!isGrounded)
        //     {
        //         transform.rotation = Quaternion.AngleAxis(14f, Vector3.back);
        //     }
        // }
        // if (horizontal == Vector3.zero)
        // {
        //     moveLeft = false;
        //     moveRight = false;
        // }

        if (isFlying)
        {
            IsFlying();
        }


    }

    void FixedUpdate()
    {

        if (IsGrounded())
        {
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.zero);
            anim.SetBool("isFly", false);
            if (moveRight || moveLeft)
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalk", true);
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalk", false);
            }

        }
        else
        {
            if (!moveRight && !moveLeft)
            {
                transform.rotation = Quaternion.AngleAxis(0f, Vector3.back);
            }
            anim.SetBool("isFly", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", false);
        }

    }

    public bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        return isGrounded;
    }

    public void MoveRightButtonPressed()
    {
        moveRight = true;
    }

    public void MoveRightButtonReleased()
    {
        moveRight = false;
    }

    public void MoveLeftButtonPressed()
    {
        moveLeft = true;
    }

    public void MoveLeftButtonReleased()
    {
        moveLeft = false;
    }

    public void FlyButtonPressed()
    {
        isFlying = true;
    }

    public void FlyButtonReleased()
    {
        isFlying = false;
    }

    public void MoveRight()
    {
        if (!isGrounded)
        {
            transform.rotation = Quaternion.AngleAxis(14f, Vector3.back);
        }
        transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        Vector3 horizontal = new Vector3(1f, 0f, 0f);
        transform.position = transform.position + horizontal * Time.deltaTime * speed;
    }

    public void MoveLeft()
    {
        if (!isGrounded)
        {
            transform.rotation = Quaternion.AngleAxis(-14f, Vector3.back);
        }
        transform.localScale = new Vector3(-2.3f, 2.3f, 2.3f);
        Vector3 horizontal = new Vector3(-1f, 0f, 0f);
        transform.position = transform.position + horizontal * Time.deltaTime * speed;
    }

    public void IsFlying()
    {
        if (elapsedTime > flyInterval)
        {
            soundSource.clip = flySound;
            soundSource.Play();
            rig2d.velocity = new Vector2(0, bounceSpeed);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isFly", true);
            elapsedTime = 0;
            isFlying = false;
        }

    }


    //void OnDrawGizmos()
    //{

    //    //Debug.DrawLine(groundCheck.transform.position, Vector2.zero, Color.black);

    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    //}
}