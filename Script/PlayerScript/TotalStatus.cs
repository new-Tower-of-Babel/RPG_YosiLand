using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatus : MonoBehaviour
{
    public string userName;
    public int damage;
    public float range;
    public float accuracy;
    public float attackSpeed;
    public float attackDelay;
    public float attackDelayA;
    public float attackDelayB;

    public int maxHp;
    public int currentHp;
    public int maxSp;
    public int currentSp;
    public int spIncreaseSpeed;
    public int spRechargeTime;
    public int currentSpRechargeTime;
    public int maxExp;
    public int currentExp;
    public int expIncrease;
    public bool spUsed;

    [SerializeField]
    public Image[] images_Gauge;
    public Text[] text_figure;

    private void Start()
    {
        //currentHp = maxHp;
        //currentSp = maxSp;
        //currentExp = maxExp;
    }
}
