using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string ItemName; // �������� �̸� (Ű��)
    [Tooltip("HP, SP, DP, HUNGRY, THIRSTY, SATISFY�� �����մϴ�.")]
    public string[] part; // ����
    public int[] num; // ��ġ
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    ItemEffect[] itemEffects;

    // �ʿ��� ������Ʈ
    [SerializeField]
    StatusController thePlayerStatus;
    [SerializeField]
    WeaponManager theWeaponManager;

    const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if(itemEffects[i].ItemName == _item.itemName)
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
                            case DP:
                                thePlayerStatus.IncreaseDP(itemEffects[i].num[j]);
                                break;
                            case HUNGRY:
                                thePlayerStatus.IncreaseHungry(itemEffects[i].num[j]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.IncreaseThirsty(itemEffects[i].num[j]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("�߸��� Status ������ �����Ű�� �ֽ��ϴ�");
                                break;
                        }
                    }
                    return;
                }
            }
            Debug.Log("ItemEffectDatabase�� ��ġ�ϴ� itemName �����ϴ�");
        }
    }
}
