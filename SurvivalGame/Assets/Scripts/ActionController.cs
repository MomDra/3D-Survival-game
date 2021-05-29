using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    float range; // ���� ������ �ִ� �Ÿ�

    bool pickupActivated; // ���� ������ �� true

    RaycastHit hitinfo; // �浹ü ���� ����

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����
    [SerializeField]
    LayerMask layerMask;

    // �ʿ��� ������Ʈ
    [SerializeField]
    Text actionText;
    [SerializeField]
    Inventory theInventory;

    void Update()
    {
        CheckItem();
        TryAction();
    }

    void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hitinfo.transform != null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickup>().item.itemName + " ȹ��");
                theInventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitinfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.text = hitinfo.transform.GetComponent<ItemPickup>().item.itemName + " ȹ�� " + "<color=yellow>" + "(E)" + "</color>";
        actionText.gameObject.SetActive(true);
    }

    void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
