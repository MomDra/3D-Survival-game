using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // �ʿ��� ������Ʈ
    [SerializeField]
    GunController theGunController;
    Gun currentGun;


    // �ʿ��ϸ� HUD ȣ��, �ʿ� ������ HUD ��Ȱ��ȭ
    [SerializeField]
    GameObject go_BulletHUD;

    // �Ѿ� ���� �ؽ�Ʈ�� �ݿ�
    [SerializeField]
    Text[] text_Bullet;

    private void Start()
    {
        currentGun = theGunController.GetGun();
    }

    private void Update()
    {
        CheckBullet();
    }

    void CheckBullet()
    {
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
