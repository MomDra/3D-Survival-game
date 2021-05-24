using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    // ���� ��ġ
    Vector3 originPos;

    // ���� ��ġ
    Vector3 currentPos;

    // Sway �Ѱ�
    [SerializeField]
    Vector3 limitPos;

    // ������ Sway �Ѱ�
    [SerializeField]
    Vector3 fineSightLimitPos;

    // �ε巯�� ������ ����
    [SerializeField]
    Vector3 smoothSway;

    // �ʿ��� ������Ʈ
    [SerializeField]
    GunController theGunController;

    private void Start()
    {
        originPos = this.transform.localPosition;
    }

    private void Update()
    {
        TrySway();
    }

    void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            Swaying();
        else
            BackToOriginPos();
    }

    void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");
        Vector3 limit;

        if (!theGunController.GetFineSightMode())
            limit = limitPos;
        else
            limit = fineSightLimitPos;
            
        smoothSway.x = limit.z;

        currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limit.x, limit.x),
                       Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveX, smoothSway.y), -limit.y, limit.y),
                       originPos.z);


        transform.localPosition = currentPos;
    }

    void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}
