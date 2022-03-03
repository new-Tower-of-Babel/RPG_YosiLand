using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_Base : MonoBehaviour
{
    [SerializeField]
    protected int monsterHP;
    [SerializeField]
    protected int monsterDamage;
    [SerializeField]
    protected float attackDelay;
    [SerializeField]
    protected LayerMask targetMask;
    [SerializeField]
    protected float ChaseTime;
    protected float currentChaseTime;
    [SerializeField]
    protected float ChaseDelayTime;
    [SerializeField]
    protected float RunSpeed;
    protected int drop_money;

    protected Vector3 destination;

    protected bool isAction;
    protected bool isRunning;
    protected bool isChasing;
    protected bool isAttacking;
    protected bool isDead;
    protected bool isReturn;

    [SerializeField]
    protected float waitTime;
    protected float currentTime;
    protected Vector3 originPos;

    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected BoxCollider boxCol;
    protected NavMeshAgent nav;
    protected FieldOfViewAngle theViewAngle;
    protected StatusController thePlayerStatus;
    [SerializeField]
    protected GameObject monsterPrefab;
    [SerializeField]
    protected GameObject combi_Ingre1;
    [SerializeField]
    protected GameObject combi_Ingre2;
    [SerializeField]
    protected GameObject hP_Potion1;
    [SerializeField]
    protected GameObject hP_Potion2;
    [SerializeField]
    protected GameObject sP_Potion1;
    [SerializeField]
    protected GameObject sP_Potion2;
    [SerializeField]
    protected GameObject Field1_Allocation;
    [SerializeField]
    protected Inventory theInventory;
    

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStatus = FindObjectOfType<StatusController>();
        theViewAngle = GetComponent<FieldOfViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
        originPos = transform.position;
        Debug.Log("originpos" + originPos);
    }

    // Update is called once per frame
    protected void Update()
    {
        Zone_return();
        if (!isDead&&!isReturn)
        {
            if (theViewAngle.View() && !isAttacking)
            {
                StopAllCoroutines();
                StartCoroutine(ChaseTargetCoroutine());
            }
        }
    }
    public void Damage(int _dmg)
    {
        if (!isDead && !isReturn)
        {
            monsterHP -= _dmg;
            if (monsterHP <= 0)
            {
                if (monsterHP <= 0)
                {
                    Dead();
                    return;
                }
                anim.SetTrigger("Get Hit");
            }
        }
    }
    protected virtual void Dead()
    {
        drop_money = Random.Range(1, 100);
        isRunning = false;
        isChasing = false;
        isAttacking = false;
        isReturn = false;
        isDead = true;
        anim.SetTrigger("Die");
        theInventory.Money_Calc(drop_money);
    }
    public void Chase(Vector3 _targetPos)
    {
        if (!isReturn)
        {
            isChasing = true;
            destination = _targetPos;
            nav.speed = RunSpeed;
            isRunning = true;
            anim.SetBool("Run", isRunning);
            nav.SetDestination(destination);
            if(isReturn)
                nav.SetDestination(originPos);
        }

    }
    protected IEnumerator ChaseTargetCoroutine()
    {
        if (!isDead&&!isReturn)
        {
            currentChaseTime = 0;
            while (currentChaseTime < ChaseTime)
            {
                Chase(theViewAngle.GetTargetPos());
                if (Vector3.Distance(transform.position, theViewAngle.GetTargetPos()) < 5f)
                {
                    if (theViewAngle.View())
                    {
                        Debug.Log("플레이어 공격 시도");
                        StartCoroutine(AttackCoroutine());
                    }
                }
                yield return new WaitForSeconds(ChaseDelayTime);
                currentChaseTime += ChaseDelayTime;
            }
            isChasing = false;
        }
    }
    protected IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        nav.ResetPath();
        currentChaseTime = ChaseTime;
        yield return new WaitForSeconds(0.1f);
        transform.LookAt(theViewAngle.GetTargetPos());
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        RaycastHit _hit;
        if(Physics.Raycast(transform.position,transform.forward, out _hit, 3, targetMask))
        {
            Debug.Log("처맞음");
            thePlayerStatus.DecreaseHP(monsterDamage);
        }
        else
        {
            Debug.Log("피함?");
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        StartCoroutine(ChaseTargetCoroutine());
    }
    protected void Zone_return()
    {
        if (!isDead)
        {
            RaycastHit _zonehit;
            if (Physics.Raycast(transform.position, transform.forward, out _zonehit, 3))
            {
                if (_zonehit.transform.name == "BoundaryWall1")
                {
                    //isChasing = false;
                    //isAttacking = false;
                    //isAction = false;
                    //isRunning = false;
                    isReturn = true;
                    nav.SetDestination(originPos);
                    Debug.Log("zonereturn");
                    Debug.Log(destination);
                }
                if(_zonehit.transform.name== "nedBoarPBR_BossZone")
                {
                    isChasing = false;
                    isAttacking = false;
                    isAction = false;
                    isRunning = false;
                    isReturn = false;
                    anim.SetBool("Run", isRunning);
                    nav.ResetPath();
                }
            }
        }
    }
}
