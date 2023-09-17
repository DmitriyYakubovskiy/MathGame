using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public TextMeshProUGUI textMark;

    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] protected LayerMask ground;
    [SerializeField] private Transform Panel;
    
    private new Rigidbody2D rigidbody;
    private new Transform transform;
    private Animator Animator { get; set; }

    private Vector2 moveVector;
    private Vector2 previousPosition;

    private bool isGrounded;
    private bool isJumped;

    private int jumpForce = 15;

    private float radiusCheckGround = 0.2f;
    private float timeBtwJump = 0;
    private float startTimeBtwJump = 0.2f;
    private float speed = 10;

    public int CountOfTask;
    public int CountOfTaskComplete { get;set; }
    public int CountOfMistake { get; set; }

    public void PrintResult()
    {
        if (CountOfTaskComplete == CountOfTask)
        {
            int mark = CreateMark();
            text.text = $"Количество ошибок: {CountOfMistake}\nОценка: ";
            textMark.text = $"{CreateMark()}";
            textMark.color = Color.red;
            if (mark > PlayerPrefs.GetInt("level" + SceneManager.GetActiveScene().buildIndex)) PlayerPrefs.SetInt("level" + SceneManager.GetActiveScene().buildIndex, mark);
        }
    }

    public int CreateMark()
    {
        if (((float)CountOfTask / 100) * 90 < CountOfTask - CountOfMistake) return 5;
        if (((float)CountOfTask / 100) * 70 < CountOfTask - CountOfMistake) return 4;
        if (((float)CountOfTask / 100) * 60 < CountOfTask - CountOfMistake) return 3;
        return 2;
    }

    private States State
    {
        get { return (States)Animator.GetInteger("State"); }
        set { Animator.SetInteger("State", (int)value); }
    }


    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>(); 
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Math.Abs(fixedJoystick.Horizontal) >= 0.1f)
        {
            moveVector=new Vector2(fixedJoystick.Horizontal,0);
            Move();
        }

        RechargeTimeJump();
    }

    private void FixedUpdate()
    {
        if (isJumped && isGrounded)
        {
            Jump();
            timeBtwJump = startTimeBtwJump;
        }

        Animator.speed = 1;

        if (isGrounded)
        {
            if (Math.Abs(fixedJoystick.Horizontal) >= 0.1f)
            {
                State = States.Run;
                Animator.speed = Math.Abs(fixedJoystick.Horizontal);
            }
            else
            {
                State = States.Idle;
            }
        }
        else
        {
            isJumped = false;
            if (previousPosition.y + 0.09f < rigidbody.position.y)
            {
                State = States.Jump;
            }
        }

        CheckGround();
        previousPosition = new Vector2(rigidbody.position.x, rigidbody.position.y);
    }
    public void JumpButton()
    {
        isJumped = true;
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Move()
    {
        if (moveVector.x < 0)
        {
            SetFlip(true);
        }
        else if (moveVector.x != 0)
        {
            SetFlip(false);
        }

        rigidbody.velocity = new Vector2(moveVector.x * speed, rigidbody.velocity.y);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(new Vector2(rigidbody.position.x, rigidbody.position.y), radiusCheckGround, ground);
        isGrounded = collider.Length > 0;
    }


    private bool RechargeTimeJump()
    {
        if (timeBtwJump > 0)
        {
            timeBtwJump -= Time.deltaTime;
            return false;
        }
        return true;
    }

    private void SetFlip(bool flip)
    {
        if (flip == true)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
} 


