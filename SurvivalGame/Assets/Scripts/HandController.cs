using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Ȱ��ȭ ����
    public static bool isActivate;

    // ���� ������ Hand�� Ÿ�� ����
    [SerializeField]
    Hand currentHand;

    // ������
    bool isAttack;
    bool isSwing;

    RaycastHit hitinfo;

    private void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                // �ڷ�ƾ ����
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayA - currentHand.attackDelayB);

        isAttack = false;
    }

    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (checkObject())
            {
                isSwing = false;
                // �浹 ��
                Debug.Log(hitinfo.transform.name);
            }
            else
            {

            }
            yield return null;
        }
    }

    bool checkObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitinfo, currentHand.range))
        {
            return  true;
        }
        return false;
    }

    public void HandChange(Hand _hand)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentHand = _hand;
        WeaponManager.currentWeapon = currentHand.transform;
        WeaponManager.currentWeaponAnim = currentHand.anim;

        currentHand.transform.localPosition = Vector3.zero;
        currentHand.gameObject.SetActive(true);
        isActivate = true;
    }
}
