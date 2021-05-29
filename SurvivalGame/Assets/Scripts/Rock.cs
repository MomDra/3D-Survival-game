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
    [SerializeField]
    GameObject go_rock_item_prefab; // ������ ������

    // ������ ������ ���� ����
    [SerializeField]
    int Maxcount;

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

        int count = Random.Range(1, Maxcount + 1);

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
            Debug.Log("�� �ϳ���");
        }
        
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
