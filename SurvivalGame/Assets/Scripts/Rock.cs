using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    public int hp; // 바위의 체력

    [SerializeField]
    float destroyTime; // 파편 제거 시간

    [SerializeField]
    SphereCollider col; // 구체 컬라이더

    // 필요한 게임 오브젝트
    [SerializeField]
    GameObject go_rock; // 일반 바위
    [SerializeField]
    GameObject go_debris; // 깨진 바위
    [SerializeField]
    GameObject go_effect_prefab; // 채굴 이펙트
    [SerializeField]
    GameObject go_rock_item_prefab; // 돌맹이 아이템

    // 돌맹이 아이템 등장 개수
    [SerializeField]
    int Maxcount;

    // 필요한 사운드 이름
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
            Debug.Log("왜 하나야");
        }
        
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
