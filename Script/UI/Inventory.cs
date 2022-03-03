using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    [SerializeField]
    private GameObject go_inventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    private Slot[] slots;
    [SerializeField]
    private Text money_Text;
    [SerializeField]
    public int money_count;
    public Slot[] GetSlots() { return slots; }
    [SerializeField]
    private Item[] items;
    public void LoadToInven(int _arrayNum,string _itemName,int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
                slots[_arrayNum].Additem(items[i], _itemNum);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        Money_dialouge();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }
    private void OpenInventory()
    {
        go_inventoryBase.SetActive(true);
    }
    private void CloseInventory()
    {
        go_inventoryBase.SetActive(false);
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.Itemtype.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].Additem(_item, _count);
                return;
            }
        }
    }
    private void Money_dialouge()
    {
        money_Text.text = money_count.ToString();
    }
    public void Money_Calc(int _count)
    {
        money_count += _count;
        money_Text.text = money_count.ToString();
    }
}
