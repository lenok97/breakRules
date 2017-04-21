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

        private static int maxLives = 20;
        private float rate = 0.7F;
        private LivesBar livesBar;
        private bool isGrounded = false;
        new private Rigidbody2D rigidbody;
        private Animator animator;
        private ShootingType.BulletType ShootingState;

        //rules controllers
        ChangeGravity gravityController = new ChangeGravity();
        ChangeJump jumpController = new ChangeJump();
        ChangeShell shellController = new ChangeShell();


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

        private Bullet bullet;

        private UnitState.CharState State
        {
            get { return (UnitState.CharState)animator.GetInteger("State"); }
            set { animator.SetInteger("State", (int)value); }
        }

        private void Awake()
        {
            livesBar = FindObjectOfType<LivesBar>();

            ruleController.Add(gravityController);
            ruleController.Add(jumpController);
            ruleController.Add(shellController);

            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            rules = new Rules();
            bullet = Resources.Load<Bomb>("Bomb");
        }



        private void FixedUpdate()
        {
            CheckGround();
            if (isGrounded)
            {
                State = UnitState.CharState.Idle;
            }
            //foreach (IRule controller in ruleController)
            //{
            //    controller.Renew(rules);
            //}
        }

        public void Move(float value)
        {
            if (rules.frozen)
            {
                return;
            }
            if (isGrounded)
            {
                State = UnitState.CharState.Run;
            }

            var direction = Vector2.right * (Mathf.Sign(value) * Mathf.Sqrt(Mathf.Abs(value)));
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3)direction, rules.speed * Time.deltaTime);
            sprite.flipX = direction.x < 0;
        }

        public void Jump()
        {
            if (!isGrounded || !rules.canJump)
            {
                return;
            }
            State = UnitState.CharState.Jump;
            rigidbody.AddForce(transform.up * rules.jumpForce, ForceMode2D.Impulse);
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
            transform.Rotate(rules.gravityRotate);
            rigidbody.gravityScale *= -1;
        }

        public void ChangeShooting()
        {
            ShootingState++;
            
            if ((int)ShootingState > 2)
                ShootingState = 0;

            if ((int)ShootingState == 0)
                bullet = Resources.Load<Bomb>("Bomb");
            else

            if ((int)ShootingState == 1)
                bullet = Resources.Load<Laser>("Laser");
            else

            if ((int)ShootingState == 2)
                bullet = Resources.Load<Stun>("Stun");
        }

        public void DisableJump()
        {
            rules.canJump = !rules.canJump;
        }

        public void ActivateShell()
        {
            rules.shell = !rules.shell;
        }

        public void DisableVisible()
        {
            rules.visibly = !rules.visibly;
            Cam.GetComponent<Camera>().cullingMask = rules.visibly ? -1 : 0;
        }

        public override void ReceiveDamage()
        {
            if (!rules.shell)                         //проверка на наличие щита
            {
                Lives--;
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
            }
            else rules.shell = false;
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