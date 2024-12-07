using UnityEngine;
using UnityEngine.AI;
using Microsoft.CSharp;
using System.Buffers;

public class Enemy : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    private float maxStamina;
    private float currentStamina;
    private bool canAttack;
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private float viewLength;
    public bool IsKnockBack;
    [SerializeField]
    private LayerMask targetLayer;


    private Vector3 patrolPos;

    [SerializeField]
    private Transform playerTF;
    private Animator animator;


    void Start()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindPlayer())
        {
            Patrol();
            return;
        }
        if(!MoveTo())
        {
            return;
        }
        if(Attack())
        {
            return;
        }
        if(Guard())
        {
            return;
        }


        
    }

    private void FixedUpdate()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void Damage(float _damage)
    {
        if (!canAttack)
        {
            currentHP -= _damage * 1.2f;

        }
        else
        {
            currentHP -= _damage;
        }
        canAttack = false;
        if (currentHP < 0)
        {
            currentHP = 0;
        }
        animator.SetTrigger("Damage");
    }

    private void Patrol()
    {
        Wait();
    }

    private void Wait()
    {

    }

    private bool Guard()
    {
        bool _value = false;
        return _value; 
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle = transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    private bool FindPlayer()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewLength, targetLayer);


        if (_target.Length <= 0)
        {
            playerTF = null;
            return false;
        }
        
        Vector3 _direction = (_target[0].transform.position - transform.position).normalized;
        float _angle = Vector3.Angle(_direction, transform.forward);
        Debug.LogWarning(_angle);
        if(_angle > viewAngle * 0.5f)
        {
            playerTF = null;
            return false;
        }
        RaycastHit _hit;
        if(!Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewLength))
        {
            playerTF = null;
            return false;
        }
        if (transform.forward.normalized != _direction)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _angle, transform.rotation.eulerAngles.z);
            transform.forward = _direction;
        }
        playerTF = _target[0].transform;
        return true;
    }
    



    private bool Attack()
    {
        if (!canAttack)
        {
            return false;
        }

        animator.SetTrigger("Attack");
        canAttack = false;
        return true;
    }

    private bool MoveTo()
    {
        float _distance = (transform.position - playerTF.position).magnitude;
        Debug.Log(_distance);
        if(_distance <= 1.6f)
        {
            animator.SetBool("Move", false);
            return true;
        }
        animator.SetBool("Move", true);
        animator.SetFloat("Y", 1);

        return false;
    }

    public void ActionEnded()
    {
        canAttack = true;
    }
}
