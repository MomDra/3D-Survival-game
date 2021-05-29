using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    public int hp; // ������ ü��

    [SerializeField]
    float destroyTime; // ���� ���� �ð�

    [SerializeField]
    SphereCollider col; // ��ü �ö��̴�

    // �ʿ��� ���� ������Ʈ
    [SerializeField]
    GameObject go_rock; // �Ϲ� ����
    [SerializeField]
    GameObject go_debris; // ���� ����
    [SerializeField]
    GameObject go_effect_prefab; // ä�� ����Ʈ

    // �ʿ��� ���� �̸�
    [SerializeField]
    string strike_Sound;
    [SerializeField]
    string destroy_Sound;

    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);

        var clone = Instantiate(go_effect_prefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destrouction();
    }

    void Destrouction()
    {
        SoundManager.instance.PlaySE(destroy_Sound);

        col.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
