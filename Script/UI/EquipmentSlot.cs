using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    public Image itemImage;
    [SerializeField]
    public Slot theSlot;
    [SerializeField]
    public TotalStatus totalStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

}
