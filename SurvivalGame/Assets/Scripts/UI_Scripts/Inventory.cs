using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    // ÇÊ¿äÇÑ ÄÄÆ÷³ÍÆ®
    [SerializeField]
    GameObject go_InventoryBase;
    [SerializeField]
    GameObject go_SlotsParent;

    // ½½·Ôµé
    Slot[] slots;

    public Slot[] GetSlots() { return slots; }

    [SerializeField]
    Item[] items;

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].itemName == _itemName)
            {
                slots[_arrayNum].Additem(items[i], _itemNum);
            }
        }
    }

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            GameManager.isOpenInventory = inventoryActivated;
            go_InventoryBase.SetActive(inventoryActivated);
        }
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item)
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
            if (!slots[i].item)
            {
                slots[i].Additem(_item, _count);
                return;
            }
        }
    }
}
