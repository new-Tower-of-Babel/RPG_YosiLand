using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;


    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walk", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("Run", _flag);
    }
    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public void GetAnimation()
    {
        animator.SetTrigger("Get");
    }
    public void DieAnimation()
    {
        animator.SetTrigger("Die");
    }
    public void DieRecoverAnimation()
    {
        animator.SetTrigger("DieRecover");
    }
}
