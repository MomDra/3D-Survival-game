using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // ���� �ߺ� ��ü ���� ����
    public static bool isChangeWeapon;

    // ���繫��� ���繫���� �ִϸ��̼�
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    // ���� ������ Ÿ��
    [SerializeField]
    string currentWeaponType;

    // ���� ��ü ������
    [SerializeField]
    float changeWeaponDelayTime;
    // ���� ��ü�� ������ ���� ����
    [SerializeField]
    float changeWeaponEndDelayTime;

    // ���� ������ ���� ����
    [SerializeField]
    Gun[] guns;
    [SerializeField]
    CloseWeapon[] hands;
    [SerializeField]
    CloseWeapon[] axes;
    [SerializeField]
    CloseWeapon[] pickaxes;

    // ���� �������� ���� ���� ������ �����ϵ��� ����
    Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    Dictionary<string, CloseWeapon> handWeaponDictionary = new Dictionary<string, CloseWeapon>();
    Dictionary<string, CloseWeapon> axeWeaponDictionary = new Dictionary<string, CloseWeapon>();
    Dictionary<string, CloseWeapon> pickaxeWeaponDictionary = new Dictionary<string, CloseWeapon>();


    // �ʿ��� ������Ʈ
    [SerializeField]
    GunController theGunController;
    [SerializeField]
    HandController theHandController;
    [SerializeField]
    AxeController theAxeController;
    [SerializeField]
    PickaxeController thePickaxeController;

    private void Start()
    {
        int i = 0;

        for (i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }

        for (i = 0; i < hands.Length; i++)
        {
            handWeaponDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }

        for (i = 0; i < axes.Length; i++)
        {
            axeWeaponDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }

        for (i = 0; i < pickaxes.Length; i++)
        {
            pickaxeWeaponDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
    }

    private void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            else if(Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            else if(Input.GetKeyDown(KeyCode.Alpha3))
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                StartCoroutine(ChangeWeaponCoroutine("HAND", "Hand"));
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                theGunController.CancelFineSight();
                theGunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController.isActivate = false;
                break;
            case "HAND":
                HandController.isActivate = false;
                break;
        }
    }

    void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
            theGunController.GunChange(gunDictionary[_name]);
        else if (_type == "AXE")
            theAxeController.CloseWeaponChange(axeWeaponDictionary[_name]);
        else if(_type == "PICKAXE")
            thePickaxeController.CloseWeaponChange(pickaxeWeaponDictionary[_name]);
        else if (_type == "HAND")
            theHandController.CloseWeaponChange(handWeaponDictionary[_name]);  
    }
}
