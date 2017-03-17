using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharState
{
    Idle,
    Run,
    Jump
}

public class Character : Unit
{
    [SerializeField]
    public GameObject Cam;
    public bool visibly=true;
    public bool shell=false;
    private static int maxLives = 20;
    public static int MaxLives
    {
        get { return maxLives; }
    }

    private int lives = maxLives;

    public int Lives
    {
        get { return lives; }
        set 
        {
            if (value < MaxLives)
            lives=value;
            livesBar.Refresh();
        }
    }

    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    
    [SerializeField]
    private float jumpForce = 15.0F;

    private bool isGrounded = false;

    private Bullet bullet;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value);}
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = CharState.Idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetKeyDown("i"))          //невидимость
        {
            if (visibly)
            {
                visibly = false;
                Cam.GetComponent<Camera>().cullingMask = 0;
            }
            else
            {
                visibly = true;
                Cam.GetComponent<Camera>().cullingMask = -1;

            }
        }
        if (Input.GetKeyDown("s"))          //щит
        {
            if (!shell)
            {
                shell = true;
            }
            else
            {
                shell = false;
            }
        }
    }

    private void Run()
    {
        if (isGrounded) State = CharState.Run;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0;

    }

    private void Jump()
    {
        State = CharState.Jump;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.3F;
        position.x += 0.2F;

        Bullet newBullet=Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);

    }

    public override void ReceiveDamage()
    {
        
        if (!shell)                         //проверка на наличие щита
        {
            Lives--;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
            Debug.Log(lives);
        }
        else shell = false;

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;
        if (!isGrounded) State = CharState.Jump;
    }
}



