using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    private float maxStamina;
    private float currentStamina;
    private bool canAttack;

    private RaycastHit hit;
    [SerializeField]
    private LayerMask layer;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindPlayer())
        {
            Patrol();
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

    public void Damage(float _damage)
    {

    }

    private void Patrol()
    {
        
    }

    private void Wait()
    {

    }

    private bool Guard()
    {
        bool _value = false;
        return _value; 
    }
    private bool FindPlayer()
    {
        bool _found = false;
        _found = Physics.BoxCast(transform.position, new Vector3(0f, 0f, 0f), transform.forward, Quaternion.identity, 10f, layer);
        
        Debug.Log(_found);


        return _found;
    }
    



    private bool Attack()
    {
        bool _found = false;
        return _found;
    }
}
