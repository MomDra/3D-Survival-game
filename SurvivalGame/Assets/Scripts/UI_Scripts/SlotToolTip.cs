using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    GameObject go_Base;

    [SerializeField]
    Text txt_ItemName;
    [SerializeField]
    Text txt_ItemDesc;
    [SerializeField]
    Text txt_ItemItemHowtoUse;


    RectTransform rectTransform_go_Base;
    private void Start()
    {
        rectTransform_go_Base = go_Base.GetComponent<RectTransform>();
    }
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        _pos += new Vector3(rectTransform_go_Base.rect.width * 0.5f, -rectTransform_go_Base.rect.width * 0.3f, 0);
        go_Base.transform.position = _pos;
        go_Base.SetActive(true);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemItemHowtoUse.text = "우클릭 - 장착";
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemItemHowtoUse.text = "우클릭 - 먹기";
        else
            txt_ItemItemHowtoUse.text = "";
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
