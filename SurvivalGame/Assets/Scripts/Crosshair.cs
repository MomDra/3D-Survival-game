using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    // 크로스헤어 상태에 따른 총의 정확도
    float gunAccuracy;

    // 크로스 헤어 비활성화를 위한 부모 객체
    [SerializeField]
    GameObject go_CrosshairHUD;
    [SerializeField]
    GunController theGunController;

    public void WalkAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        anim.SetBool("Walk", _flag);
    }

    public void RunAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        anim.SetBool("Run", _flag);
    }

    public void JumpAnimation(bool _flag)
    {
        anim.SetBool("Run", _flag);
    }

    public void CrouchAnimation(bool _flag)
    {
        anim.SetBool("Crouch", _flag);
    }

    public void FineSightAnimation(bool _flag)
    {
        anim.SetBool("FineSightMode", _flag);
    }

    public void FireAnimation()
    {
        if (anim.GetBool("Walk"))
            anim.SetTrigger("Walk_Fire");
        else if (anim.GetBool("Crouch"))
            anim.SetTrigger("Crouch_Fire");
        else
            anim.SetTrigger("Idle_Fire");
    }

    public float GetAccuracy()
    {
        if (theGunController.GetFineSightMode())
            gunAccuracy = 0.0001f;
        else if (anim.GetBool("Run"))
            gunAccuracy = 0.5f;
        else if (anim.GetBool("Crouch"))
            gunAccuracy = 0.0015f;
        else if (anim.GetBool("Walk"))
            gunAccuracy = 0.06f;
        else
            gunAccuracy = 0.035f;


        Debug.Log(gunAccuracy);
        return gunAccuracy;
    }
}
