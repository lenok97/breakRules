using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IEnumerable
{
#region Переменные
    [SerializeField]
    private float coefficientSpeed = 3F;
    public float CoefficientSpeed { get { return coefficientSpeed; } }

    [SerializeField]
    private float coefficientJump = 7;
    public float CoefficientJump { get { return coefficientJump; } }

    [SerializeField]
    private float minGroundNormalY = .65f;

    /// <summary>
    /// максимальная скорость бега.
    /// </summary>
    public float MaxSpeed { get; set; }

    /// <summary>
    /// Скорость прыжка
    /// </summary>
    public float JumpVelosity { get; set; }

    /// <summary>
    /// Задаваемая скорость движения
    /// </summary>
    public Vector2 TargetVelosity { get; set; }

    /// <summary>
    /// Наличие земли под ногоми
    /// </summary>
    public bool Grounded { get { return grounded; } }

    private Rigidbody2D rb2D;
    public Rigidbody2D Rb2d { get { return rb2D; } }

    protected SpriteRenderer spriteRenderer;
    protected float gravityDirection = 1;
    public float GravityDirection { get { return gravityDirection; } }
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;
    protected Vector2 groundNormal;
    protected Vector2 velocity;    

    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[8];    
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(8);
    private bool grounded;
    //private List<ContactPoint2D> contactList = new List<ContactPoint2D>(8);
    //private ContactPoint2D[] contact= new ContactPoint2D[8];   

    /// <summary>
    /// Ссылка на последнего созданного юнита
    /// </summary>
    public static Unit LastCreated = null;
    /// <summary>
    /// Ссылка на первого созданного юнита
    /// </summary>
    public static Unit FirstCreated = null; 
    /// <summary>
    /// Ссылка на следующего юнита в списке
    /// </summary>
    public Unit NextUnit { get; set; }
    /// <summary>
    /// Ссылка на предыдущего юнита в списке
    /// </summary>
    public Unit PrevUnit { get; set; }
    #endregion
#region Загрузка юнита
    private void Awake()
    {
        AddUnit();
        rb2D = GetComponent<Rigidbody2D>();
        MaxSpeed = coefficientSpeed;
        JumpVelosity = coefficientJump;
    }

    void Start()
    {
        
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    #endregion
#region Методы
    void FixedUpdate()
    {
        grounded = false;
        CheckGround();
        //Изменяем направление движения так чтобы оно было параллельно земли
        velocity=TargetVelosity.x* new Vector2(groundNormal.y, -groundNormal.x);
        velocity = velocity * gravityDirection;
        //Проверяем можно ли двигаться вперед
        Movement(velocity *MaxSpeed* Time.fixedDeltaTime);
    }

    /// <summary>
    /// Проверяет наличие земли под ногами
    /// </summary>
    void CheckGround()
    {
        //слишком точно определяет нормаль к земле. Стоит применять толко если идеально сделаны колайдеры(без единого заусенца).
        /*int count = rb2D.GetContacts(contactFilter, contact);
        contactList.Clear();
        for (int i = 0; i < count; i++)
        {
            contactList.Add(contact[i]);
        }
        for (int i = 0; i < contactList.Count; i++)
        {
            Vector2 currentNormal = contactList[i].normal;
            if (currentNormal.y * gravityDirection > minGroundNormalY)
            {
                grounded = true;
                groundNormal = currentNormal;
            }
        }*/
        int count = rb2D.Cast(Vector2.down*gravityDirection, contactFilter, hitBuffer, shellRadius);
        hitBufferList.Clear();
        for (int i = 0; i < count; i++)
        {
            hitBufferList.Add(hitBuffer[i]);
        }
        for (int i = 0; i < hitBufferList.Count; i++)
        {
            Vector2 currentNormal = hitBufferList[i].normal;
            if (currentNormal.y*gravityDirection > minGroundNormalY)
            {
                grounded = true;
                groundNormal = currentNormal;
            }
        }
        if (count == 0)
            groundNormal = gravityDirection*Vector2.up;
    }

    /// <summary>
    /// Проверка наличие стены на пути юнита.
    /// </summary>
    /// <param name="move">Скорость юнита</param>
    void Movement(Vector2 move)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                //Если это стена изменяем растояние перемещение
                if (hitBufferList[i].normal.y*gravityDirection < minGroundNormalY)
                {
                    float modifiedDistance = hitBufferList[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance; 
                }
                
            }
        }
        //Если растояние премещения меньше допустимого обнуляем velocity, нужно для того чтобы анимация бега остановилась
        if (distance < minMoveDistance)
        {
            velocity = Vector2.zero;
        }
        //Премещаем юнит
        rb2D.position = rb2D.position + move.normalized * distance;
    }
    private void OnDestroy()
    {
        Controller.Instance.RemoveUnit(this);
        RemoveUnit();
    }

    public void ChengeGravity()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //transform.RotateAround(rb2D.position, Vector3.forward, 180);
        //transform.RotateAround(rb2D.position, Vector3.up, 180);
        transform.Rotate(new Vector3(0, 180, 180));
        rb2D.gravityScale *= -1;
        gravityDirection *= -1;
    }
#endregion
#region Реализация Enumerator
    private void RemoveUnit()
    {
        // Переустановить ссылки, если уничтожается объект в середине цепочки
        if (PrevUnit != null)
            PrevUnit.NextUnit = NextUnit;

        if (NextUnit != null)
            NextUnit.PrevUnit = PrevUnit;
    }
    private void AddUnit()
    {
        //Обновить ссылку на первого созданного юнита?
        if (FirstCreated == null)
        {
            FirstCreated = this;
            Unit.LastCreated = this;
        }
        else
        if (this.CompareTag("Player"))
        {
            NextUnit = FirstCreated;
            FirstCreated.PrevUnit = this;
            FirstCreated = this;
        }
        else
        {
            //Обновить ссылку на последнего созданного юнита?
            if (Unit.LastCreated != null)
            {
                Unit.LastCreated.NextUnit = this;
                PrevUnit = Unit.LastCreated;
            }
            Unit.LastCreated = this;
        }
    }
    //Возращает данный класс как итератор
    public IEnumerator GetEnumerator()
    {
        return new UnitEnumerator();
    }
#endregion
}
