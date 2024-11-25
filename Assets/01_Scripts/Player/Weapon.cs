using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator Animator;
    
    
    [SerializeField]
    private float knockBackTime;


    private void Start()
    {
    }

    private void Update()
    {
        if (!GetComponentInParent<PlayerController>().IsKnockBack)
        {
            Animator.StopPlayback();
            return;
        }
        if(knockBackTime < 0.5f)
        {
            knockBackTime += Time.deltaTime;
            return;
        }

        GetComponentInParent<PlayerController>().IsKnockBack = false;
        knockBackTime = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 6 && other.gameObject.layer != 7)
        {
            return;
        }

        if(other.gameObject.layer == 7)
        {
            //Read Enemys Component, consume enemys stamina and return.
        }
        Animator.StopPlayback();
        GetComponentInParent<PlayerController>().IsKnockBack = true;
    }

    
}
