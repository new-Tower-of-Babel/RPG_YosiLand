using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    public TotalStatus totalStatus;
    private const int image_HP = 0, image_SP = 1, image_EXP = 2;
    private const int text_maxHP = 0, text_currentHP = 1, text_maxSP = 2, text_currentSP = 3;
    private const int text_EXPpercent = 4;
    private float expPercent;
    // Start is called before the first frame update
    void Start()
    {
        totalStatus = GetComponent<TotalStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }
    public void SPRechargeTime()
    {
        if (totalStatus.spUsed)
        {
            if (totalStatus.currentSpRechargeTime < totalStatus.spRechargeTime)
                totalStatus.currentSpRechargeTime++;
            else
                totalStatus.spUsed = false;
        }
    }
    private void SPRecover()
    {
        if (!totalStatus.spUsed && totalStatus.currentSp < totalStatus.maxSp)
            totalStatus.currentSp += totalStatus.spIncreaseSpeed;
    }
    public void DecreaseSP(int _count)
    {
        totalStatus.spUsed = true;
        totalStatus.currentSpRechargeTime = 0;
        if (totalStatus.currentSp - _count > 0)
            totalStatus.currentSp -= _count;
        else
            totalStatus.currentSp = 0;
    }
    private void GaugeUpdate()
    {
        totalStatus.images_Gauge[image_HP].fillAmount = (float)totalStatus.currentHp / totalStatus.maxHp;
        totalStatus.images_Gauge[image_SP].fillAmount = (float)totalStatus.currentSp / totalStatus.maxSp;
        totalStatus.images_Gauge[image_EXP].fillAmount = (float)totalStatus.currentExp / totalStatus.maxExp;
        totalStatus.text_figure[text_maxHP].text = totalStatus.maxHp.ToString();
        totalStatus.text_figure[text_currentHP].text = totalStatus.currentHp.ToString();
        totalStatus.text_figure[text_maxSP].text = totalStatus.maxSp.ToString();
        totalStatus.text_figure[text_currentSP].text = totalStatus.currentSp.ToString();
        expPercent = (float)totalStatus.currentExp / totalStatus.maxExp;
        totalStatus.text_figure[text_EXPpercent].text = expPercent.ToString();
    }
    public int GetCurrentSP()
    {
        return totalStatus.currentSp;
    }
    public void IncreaseHP(int _count)
    {
        if (totalStatus.currentHp + _count < totalStatus.maxHp)
            totalStatus.currentHp += _count;
        else
            totalStatus.currentHp = totalStatus.maxHp;
    }
    public void DecreaseHP(int _count)
    {
        totalStatus.currentHp -= _count;
        if (totalStatus.currentHp <= 0)
            Debug.Log("キャラクターの hp0");
    }
    public void IncreaseSP(int _count)
    {
        if (totalStatus.currentSp + _count < totalStatus.maxSp)
            totalStatus.currentSp += _count;
        else
            totalStatus.currentSp = totalStatus.maxSp;
    }
}
