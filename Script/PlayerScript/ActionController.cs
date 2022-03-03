using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;
    private bool pickupActivated;
    public bool npcDIalogueActivated;
    private RaycastHit hitinfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Text itemText;
    [SerializeField]
    private Text npcText;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    public DialogueManager dialogueManager;
    [SerializeField]
    private GameObject dialoguePanel;
    public GameObject dialogueImage;

    private void Start()
    {
    }
    void Update()
    {
        CheckItem();
        TryAction();
        CheckNPC();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckItem();
            CanPickUp();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckNPC();
            CanDialogue();
        }
    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitinfo.transform != null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + "흭득했습니다.");
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitinfo.transform.gameObject);
                infoItemDisappear();
            }
        }
    }

    private void CanDialogue()
    {
        if (npcDIalogueActivated)
        {
            if (dialoguePanel.activeSelf)
            {
                dialogueImage.SetActive(true);
                dialogueManager.OnDialogue(hitinfo.transform.GetComponent<NPCsentence>().sentence);
            }
            npcDIalogueActivated = false;
            npcText.gameObject.SetActive(false);
        }
    }
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            infoItemDisappear();
    }
    private void CheckNPC()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag == "NPC")
            {
                if (dialogueImage.activeSelf == false)
                    npcInfoAppear();
            }
        }
        else
            infoNPCDisappear();
    }
    private void npcInfoAppear()
    {
        npcDIalogueActivated = true;
        npcText.gameObject.SetActive(true);
        npcText.text =　"会話" + "<color=yellow>" + "(E)" + "</Color>";
    }
    private void infoNPCDisappear()
    {
        npcDIalogueActivated = false;
        npcText.gameObject.SetActive(false);
    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        itemText.gameObject.SetActive(true);
        itemText.text = hitinfo.transform.GetComponent<ItemPickUp>().item.itemName + " 獲得 " + "<color=yellow>" + "(Space)" + "</Color>";
    }
    private void infoItemDisappear()
    {
        pickupActivated = false;
        itemText.gameObject.SetActive(false);
    }
}
