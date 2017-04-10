using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace BreakRules
{
    public class Character : Unit
    {
        [SerializeField]
        public GameObject Cam;
        public bool visibly = true;
        public bool shell = false;
        private static int maxLives = 20;
        Vector3 gravityRotate = new Vector3(0, 180, 180);

        private float rate = 0.7F;

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
                    lives = value;
                livesBar.Refresh();
            }
        }

        private LivesBar livesBar;

        [SerializeField]
        private float speed = 3.0F;

        [SerializeField]
        private float jumpForce = 15.0F;

        private bool isGrounded = false;
        private bool canJump = true;

        private Bullet bullet;

        private UnitState.CharState State
        {
            get { return (UnitState.CharState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
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

            bullet = Resources.Load<Bomb>("Bomb");
        }

        private void FixedUpdate()
        {
            CheckGround();
            if (isGrounded)
            {
                State = UnitState.CharState.Idle;
            }
        }

        /*private void Update()
        {
            if (Input.GetButtonDown("Fire1") && base.canShoot)
            {
                bullet = Resources.Load<Laser>("Laser");
                Shoot();
            }
            if (Input.GetButtonDown("Fire2") && base.canShoot)
            {
                bullet = Resources.Load<Bomb>("Bomb");
                Shoot();
            }
            if (Input.GetButtonDown("Fire3") && base.canShoot)
            {
                bullet = Resources.Load<Stun>("Stun");
                Shoot();
            }
        }*/

        public void Move(float value)
        {
            if (frozen)
            {
                return;
            }
            if (isGrounded)
            {
                State = UnitState.CharState.Run;
            }

            var direction = Vector2.right * (Mathf.Sign(value) * Mathf.Sqrt(Mathf.Abs(value)));
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)direction, speed * Time.deltaTime);
            sprite.flipX = direction.x < 0;
        }

        public void Jump()
        {
            if (!isGrounded || !canJump)
            {
                return;
            }
            State = UnitState.CharState.Jump;
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        public void Shoot()
        {
            canShoot = false;
            Invoke("CanShoot", rate);
            var position = transform.position;
            position.y += 0.3F;
            position.x += 0.2F;

            var newBullet = Instantiate(bullet, position, bullet.transform.rotation);
            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);

        }

        public void ChangeGravity()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            transform.Rotate(gravityRotate);
            rigidbody.gravityScale *= -1;
        }

        public void DisableJump()
        {
            canJump = !canJump;
        }

        public void ActivateShell()
        {
            shell = !shell;
        }

        public void DisableVisible()
        {
            visibly = !visibly;
        }

        public override void ReceiveDamage()
        {
            if (!shell)                         //проверка на наличие щита
            {
                Lives--;
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
            }
            else shell = false;
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            var bullet = collider.gameObject.GetComponent<Bullet>();
            if (bullet && bullet.Parent != this.gameObject)
            {
                ReceiveDamage();
            }
        }

        private void CheckGround()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, 1);

            isGrounded = colliders.Length > 1;
            var bulletsCount = colliders.Count(t => t.gameObject.GetComponent<Bullet>() != null);
            if (bulletsCount > 0 && bulletsCount == colliders.Length - 1)
            {
                isGrounded = false;
            }
        }
    }
}