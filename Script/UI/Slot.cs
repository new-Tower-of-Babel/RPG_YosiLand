using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_GountImage;
    private itemEffectDatabase theItemEffectDatabase;
    private Rect baseRect;
    private InputNumber theInputNumber;
    [SerializeField]
    private GameObject item_Inspector;
    [SerializeField]
    private Text Item_Inspector_Text;

    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<itemEffectDatabase>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
        theInputNumber = FindObjectOfType<InputNumber>();
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void Additem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.ItemImage;
        if (item.itemType != Item.Itemtype.Equipment)
        {
            go_GountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_GountImage.SetActive(false);
        }
        SetColor(1);
    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount <= 0)
            ClearSlot();
    }
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_GountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                theItemEffectDatabase.UsedItem(item);
                if (item.itemType == Item.Itemtype.Used)
                    SetSlotCount(-1);
                else if (item.itemType == Item.Itemtype.Equipment)
                {
                    if (item.equipmentType == Item.EquipmentType.Sowrd)
                    {

                    }
                    else if (item.equipmentType == Item.EquipmentType.Amor)
                    {

                    }
                    else if (item.equipmentType == Item.EquipmentType.Helmet)
                    {

                    }
                    else if (item.equipmentType == Item.EquipmentType.Glove)
                    {

                    }
                    else if (item.equipmentType == Item.EquipmentType.Boots)
                    {

                    }
                    else if (item.equipmentType == Item.EquipmentType.Ring)
                    {

                    }
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null&&item_Inspector.activeSelf==false)
            {
                item_Inspector.SetActive(true);
                if (item.itemType == Item.Itemtype.Equipment)
                {
                    Item_Inspector_Text.text = "ItemName" + " = " + item.itemName + "\n" +
                                                " damage " + "   =   " + item.damage + "\n" +
                                                " defense" + "    =   " + item.defense + "\n" +
                                                " speed " + "      =   " + item.speed;
                }else if (item.itemType == Item.Itemtype.Used)
                {
                    Item_Inspector_Text.text = "ItemName" + " = " + item.itemName + "\n" +
                                              "HPincrease" + " = " + item.hp_increase + "\n" +
                                              "SPincrease" + " = " + item.sp_increase + "\n";
                }else if (item.itemType == Item.Itemtype.Ingredient)
                {
                    Item_Inspector_Text.text = "ItemName" + " = " + item.itemName + "\n" +
                                                "î§ÖùªÇª¹¡£";
                }
            }else if (item_Inspector.activeSelf==true)
            {
                item_Inspector.SetActive(false);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            if (DragSlot.instance.dragSlot != null)
                theInputNumber.Call();
        }
        else
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }
    public void ChangeSlot()
    {
        Item _tempitem = item;
        int _tempItemCount = itemCount;
        Additem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if (_tempitem != null)
            DragSlot.instance.dragSlot.Additem(_tempitem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}
