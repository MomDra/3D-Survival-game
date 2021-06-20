using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate;

    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (checkObject())
            {
                if(hitinfo.transform.tag == "Rock")
                {
                    hitinfo.transform.GetComponent<Rock>().Mining();
                }
                else if(hitinfo.transform.tag == "NPC")
                {
                    SoundManager.instance.PlaySE("Animal_Hit");
                    hitinfo.transform.GetComponent<Pig>().Damage(currentWeapon.damage, transform.position);
                }
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

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }
}
