using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    // 현재 장착된 Hand형 타입 무기
    [SerializeField]
    protected CloseWeapon currentWeapon;

    // 공격중
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
                // 코루틴 실행
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

    // 미완성 = 추상 코루틴
    protected abstract IEnumerator HitCoroutine();
    
    protected bool checkObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, currentWeapon.range, layerMask))
        {
            return true;
        }
        return false;
    }

    // 완성 함수이지만, 추가 편집한 함수
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
