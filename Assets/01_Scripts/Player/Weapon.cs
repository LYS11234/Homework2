using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public Animator Animator;
     private Action Damage;


    [SerializeField]
    private float knockBackTime;
    private bool isKnockback;
    [SerializeField]
    private float attack;
   

    private void Start()
    {

    }

    private void Update()
    {
        if (!isKnockback)
        {
            Animator.StopPlayback();
            return;
        }
        if(knockBackTime < 0.5f)
        {
            knockBackTime += Time.deltaTime;
            return;
        }

        isKnockback = false;
        knockBackTime = 0;
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.Damage(attack);
        }
        else if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Damage(attack);
        }

        Animator.StopPlayback();
        isKnockback = true;
    }

    
}
