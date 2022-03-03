using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private bool isAttack = false;
    private bool isSwing = false;
    private LayerMask layerMask;

    [SerializeField]
    private TotalStatus currentTotalStats;

    //private AudioSource audioSource;

    private RaycastHit hitinfo;

    private CharacterAnimator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<CharacterAnimator>();
        currentTotalStats = GetComponent<TotalStatus>();

    }

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }
    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
                StartCoroutine(AttackCoroutine());
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        animator.AttackAnimation();
        yield return new WaitForSeconds(currentTotalStats.attackDelayA);
        isSwing = true;
        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(currentTotalStats.attackDelayB);
        isSwing = false;
        yield return new WaitForSeconds(currentTotalStats.attackDelay - currentTotalStats.attackDelayA - currentTotalStats.attackDelayB);
        isAttack = false;
    }
    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                if (hitinfo.transform.tag == "Monster")
                    hitinfo.transform.GetComponent<Monster_Base>().Damage(currentTotalStats.damage);
            }
        yield return null;
        }
    }
    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position+Vector3.up,transform.forward,out hitinfo, currentTotalStats.range))
        {
            return true;
        }
        return false;
    }
}
