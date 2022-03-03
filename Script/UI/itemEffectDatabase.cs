using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    public string[] part;
    public int[] num;
}
public class itemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;
    [SerializeField]
    private StatusController thePlayerStatus;
    private const string HP = "HP", SP = "SP";
    public void UsedItem(Item _item)
    {
        if (_item.itemType == Item.Itemtype.Used)
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHP(itemEffects[i].num[j]);
                                break;
                            case SP:
                                thePlayerStatus.IncreaseSP(itemEffects[i].num[j]);
                                break;
                            default:
                                Debug.Log("잘못된 부위");
                                break;
                        }
                        Debug.Log(_item.itemName + "을 사용했다.");
                    }
                    return;
                }
            }
            Debug.Log("아이템이펙트데이터베이스에 일치하는 이름이 없음");
        }
    }
}
