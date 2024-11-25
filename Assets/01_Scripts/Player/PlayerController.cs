using UnityEngine;

public class PlayerController : MonoBehaviour, ISubject
{
    #region Components
    [Header("Components")]
    public Animator Animator;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private BoxCollider sword;
    [SerializeField]
    private BoxCollider shield;


    [SerializeField]
    private IObserver observer;
    #endregion

    #region Classes


    #endregion

    #region Variables
    [Header("Variables")]
    public bool IsKnockBack;
    private bool knockback;
    private bool canAction = true;
    private bool attackBooked;
    private bool isDamaged;
    private float spaceInputTime;
    private float maxHP = 100;
    [SerializeField]
    private float currentHP;
    private float maxStamina = 100;
    private float currentStamina;
    private float staminaRestoreSpeed = 10;
    #endregion

    void Start()
    {
        currentHP = maxHP;
        currentStamina = maxStamina;
        Animator = GetComponent<Animator>();
        GetComponentInChildren<Weapon>().Animator = Animator;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RollAndSprint();
        GuardOn();
        Attack();
        LockOn();
        Parry();
        RestoreStamina();
        NotifyObservers();
        
    }

    
    

    private void OnAnimatorIK(int layerIndex)
    {
        if (!IsKnockBack)
        {

        }

        if (IsKnockBack)
        {
            Animator.SetTrigger("Knockback");
            IsKnockBack = false;

        }
    }

    public void AttackDelay()
    {
        sword.enabled = false;
        shield.enabled = false;
        canAction = true;
        
    }

    private void LockOn()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("ButtonDown");
        }
    }

    public void DamageEnd()
    {
        isDamaged = false;
    }

    public void Damage(float _damage)
    {
        isDamaged = true;
        currentHP -= _damage;
        if(currentHP < 0)
        {
            currentHP = 0;
        }
        Animator.SetTrigger("Damage");
    }

    public void KnockbackEnd()
    {
    }

    #region Action

    private void Attack()
    {
        if (isDamaged)
        {
            return;
        }
        if (currentStamina <= 0)
        {
            return;
        }
        if (!canAction)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackBooked = true;
            }

            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) || attackBooked)
        {
            shield.enabled = false;
            sword.enabled = true;
            currentStamina -= 10;
            attackBooked = false;
            canAction = false;
            Animator.SetTrigger("Attack");
        }
    }

    private void GuardOn()
    {
        if (!canAction)
        {
            Animator.SetBool("Guard", false);
            staminaRestoreSpeed = 10;
            return;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shield.enabled = true;
            Animator.SetBool("Guard", true);
            staminaRestoreSpeed = 5;
            return;
        }
        shield.enabled = false;
        Animator.SetBool("Guard", false);
        staminaRestoreSpeed = 10;
    }
    private void Move()
    {
        if(!canAction)
        {
            return;
        }
        if (Animator.GetBool("Guard"))
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Animator.SetBool("Move", true);
            Animator.SetFloat("Y", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("Move", true);
            Animator.SetFloat("Y", -1);
        }
        else
        {
            Animator.SetFloat("Y", 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("Move", true);
            Animator.SetFloat("X", -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            Animator.SetBool("Move", true);
            Animator.SetFloat("X", 1);
        }
        else
        {
            Animator.SetFloat("X", 0);
        }
        if (Animator.GetFloat("X") == 0 && Animator.GetFloat("Y") == 0)
        {
            Animator.SetBool("Move", false);
        }
        if(!Animator.GetBool("Move"))
        {
            return;
        }

        if (!Animator.GetBool("LockOn"))
        {
            // Y = 1: 90 * 1 - 90, A: 45 * X, D: 45 * X
            // Y = -1: -180, A: 45 * X * -1
            float _direction = (90 + 45 * Animator.GetFloat("X")) * Animator.GetFloat("Y") - 90;
            if (Animator.GetFloat("Y") == 0)
            {
                _direction = 90 * Animator.GetFloat("X");
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, cam.rotation.eulerAngles.y + _direction, 0));
        }
    }

    private void Parry()
    {
        if(!canAction)
        {
            return;
        }
        if(currentStamina <= 0)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            shield.enabled = true;
            canAction = false;
            Animator.SetTrigger("Parry");
            currentStamina -= 10;
        }

    }


    private void RollAndSprint()
    {
        if(currentStamina <= 0)
        {
            Animator.SetBool("Run", false);
            return;
        }
        if(Input.GetKey(KeyCode.Space))
        {
            spaceInputTime += Time.deltaTime;
            if(spaceInputTime > 0.3f)
            {
                if(Animator.GetBool("Guard"))
                {
                    return;
                }
                Animator.SetBool("Run", true);
                currentStamina -= 10 * Time.deltaTime;

            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(spaceInputTime > 0.3f)
            {
                spaceInputTime = 0;
                Animator.SetBool("Run", false);
                return;
            }
            Animator.SetTrigger("Roll");
        }
        
    }

    #endregion



    private void RestoreStamina()
    {
        if (canAction)
        {
            if (currentStamina >= maxStamina)
            {
                return;
            }
            if (Animator.GetBool("Run"))
            {
                return;
            }
            currentStamina += staminaRestoreSpeed * Time.deltaTime;
        }
    }
    


    public void RegisterObserver(IObserver _observer)
    {
        observer = _observer;
    }

    public void RemoveObserver(IObserver _observer)
    {
        observer = null;
    }

    public void NotifyObservers()
    {
        observer.HPCheck(currentHP / maxHP);
        observer.StaminaCheck(currentStamina / maxStamina);
    }
}
