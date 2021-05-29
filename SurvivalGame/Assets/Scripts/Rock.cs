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
        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
