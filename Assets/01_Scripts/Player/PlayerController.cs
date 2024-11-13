using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    public Animator Animator;
    public Transform RightHand;
    [SerializeField]
    private Transform rightElbow;
    [SerializeField]
    private Transform rightShoulder;
    [SerializeField]
    private Transform rightLowerArm;
    #endregion

    #region Classes


    #endregion

    #region Variables
    [Header("Variables")]
    [SerializeField]
    private Vector3 stuckHandPos = new Vector3(0.1386017f, -1.251696e-06f, 1.698727e-06f);
    [SerializeField]
    private Quaternion stuckHandRot = new Quaternion(-0.87807f, 0.16250f, -0.03113f, 0.44902f);
    [SerializeField]
    private Vector3 stuckRightShoulderPos = new Vector3(-0.01944359f, -1.117586e-08f, -1.266598e-07f);
    [SerializeField]
    private Vector3 stuckRightShoulderRotVec = new Vector3(-23.208f, -30.582f, 73.448f);
    [SerializeField]
    private Quaternion stuckRightShoulderRot;
    [SerializeField]
    private Vector3 stuckRightElbowPos = new Vector3(0.1017347f, -1.117586e-08f, -1.266598e-07f);
    [SerializeField]
    private Vector3 stuckRightElbowRotVec = new Vector3(-23.208f, -30.582f, 73.448f);
    [SerializeField]
    private Quaternion stuckRightElbowRot;
    [SerializeField]
    private Vector3 stuckRightLowerArmPos = new Vector3(0.199227f, -6.407493e-07f, 1.41561e-06f);
    [SerializeField]
    private Vector3 stuckRightLowerArmRotVec = new Vector3(-39.216f, -42.692f, 87.645f);
    [SerializeField]
    private Quaternion stuckRightLowerArmRot;
    public bool IsKnockBack;
    private bool knockback;
    #endregion

    void Start()
    {
        Animator = GetComponent<Animator>();
        stuckRightElbowRot = Quaternion.Euler(stuckRightElbowRotVec);
        stuckRightShoulderRot = Quaternion.Euler(stuckRightShoulderRotVec);
        stuckRightLowerArmRot = Quaternion.Euler(stuckRightLowerArmRotVec);
    }

    // Update is called once per frame
    void Update()
    {
        GuardOn();
        Attack();
        knockback = IsKnockBack;
    }

    private void Attack()
    {
        if (knockback)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Animator.SetTrigger("Attack");
        }
    }

    private void GuardOn()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Animator.SetBool("IsGuard", true);
            return;
        }
        Animator.SetBool("IsGuard", false);
    }

    public void CheckPosNRot()
    {
        Debug.Log($"pos = {RightHand.position}\nrot = {RightHand.localRotation}");
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!IsKnockBack)
        {
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
            return;
        }

        if (IsKnockBack)
        {
            knockback = true;
            Animator.StopPlayback();
            
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            Animator.SetIKPosition(AvatarIKGoal.RightHand, stuckHandPos);
            Animator.SetIKRotation(AvatarIKGoal.RightHand, stuckHandRot);

            Animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
            Animator.SetIKHintPosition(AvatarIKHint.RightElbow, stuckRightElbowPos);
            rightElbow.localRotation = stuckRightElbowRot;

            rightShoulder.localRotation = stuckRightShoulderRot;
            rightShoulder.localPosition = stuckRightShoulderPos;
            rightLowerArm.localPosition = stuckRightLowerArmPos;
            rightLowerArm.localRotation = stuckRightLowerArmRot;

        }
    }
}
