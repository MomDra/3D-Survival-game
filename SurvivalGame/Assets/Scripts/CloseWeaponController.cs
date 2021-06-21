using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    // ���� ������ Hand�� Ÿ�� ����
    [SerializeField]
    protected CloseWeapon currentWeapon;

    // ������
    protected bool isAttack;
    protected bool isSwing;

    protected RaycastHit hitinfo;
    [SerializeField]
    protected LayerMask layerMask;

    protected void TryAttack()
    {
        if (Input.GetButton("Fire1") && GameManager.canPlayerMove)
        {
            if (!isAttack)
            {
                // �ڷ�ƾ ����
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentWeapon.attackDelayA);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentWeapon.attackDelay - currentWeapon.attackDelayA - currentWeapon.attackDelayB);

        isAttack = false;
    }

    // �̿ϼ� = �߻� �ڷ�ƾ
    protected abstract IEnumerator HitCoroutine();
    
    protected bool checkObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, currentWeapon.range, layerMask))
        {
            return true;
        }
        return false;
    }

    // �ϼ� �Լ�������, �߰� ������ �Լ�
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
            WeaponManager.currentWeapon.gameObject.SetActive(false);

        currentWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentWeapon.transform;
        WeaponManager.currentWeaponAnim = currentWeapon.anim;

        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.gameObject.SetActive(true);
    }
}
