using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // 현재 장착된 Hand형 타입 무기
    [SerializeField]
    Hand currentHand;

    // 공격중
    bool isAttack;
    bool isSwing;

    RaycastHit hitinfo;

    private void Update()
    {
        TryAttack();
    }

    void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                // 코루틴 실행
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
                // 충돌 됨
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
}
