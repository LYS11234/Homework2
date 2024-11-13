using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Animator animator;
    
    
    [SerializeField]
    private float knockBackTime;


    private void Start()
    {
        rightHand = GetComponentInParent<PlayerController>().RightHand;
        animator = GetComponentInParent<PlayerController>().Animator;
    }

    private void Update()
    {
        if (!GetComponentInParent<PlayerController>().IsKnockBack)
        {
            animator.StopPlayback();
            return;
        }
        if(knockBackTime < 0.5f)
        {
            knockBackTime += Time.deltaTime;
            return;
        }

        GetComponentInParent<PlayerController>().IsKnockBack = false;
        knockBackTime = 0;
        //animator.Play("Idle_Normal_SwordAndShield", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            
            animator.StopPlayback();
            GetComponentInParent<PlayerController>().IsKnockBack = true;
        }
    }

    
}
