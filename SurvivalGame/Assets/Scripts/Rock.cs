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
    AudioSource audioSource;
    [SerializeField]
    AudioClip effect_sound;
    [SerializeField]
    AudioClip effect_sound2;

    public void Mining()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();
        var clone = Instantiate(go_effect_prefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destrouction();
    }

    void Destrouction()
    {
        audioSource.clip = effect_sound2;
        audioSource.Play();

        col.enabled = false;
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
