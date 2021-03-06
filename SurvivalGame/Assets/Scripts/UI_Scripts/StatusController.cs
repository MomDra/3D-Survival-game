using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    int hp;
    int currentHp;

    // 스태미나
    [SerializeField]
    int sp;
    int currentSp;

    // 스태미나 증가량
    [SerializeField]
    int spIncreaseSpeed;

    // 스태미나 재회복 딜레이
    [SerializeField]
    int spRechargeTime;
    int currentSpRechargeTime;

    // 스태미나 감소 여부
    bool spUsed;

    // 방어력
    [SerializeField]
    int dp;
    int currentDp;

    // 배고픔
    [SerializeField]
    int hungry;
    int currentHungry;

    // 배고픔이 줄어드는 속도
    [SerializeField]
    int hungryDecreaseTime;
    int currentHungryDecreaseTime;

    // 목마름
    [SerializeField]
    int thirsty;
    int currentThirsty;

    // 목마름이 줄어드는 속도
    [SerializeField]
    int thirstyDecreaseTime;
    int currentThirstyDecreaseTime;

    // 만족도
    [SerializeField]
    int satisfy;
    int currentSatisfy;

    // 필요한 이미지
    [SerializeField]
    Image[] images_Gauge;

    const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, STATISFY = 5;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentSp = sp;
        currentDp = dp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentSatisfy = satisfy;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1f)
        {
            Hungry();
            Thirsty();
            time = 0;
        }
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
    }

    private void SPRechargeTime()
    {
        if (spUsed)
        {
            if (currentSpRechargeTime < spRechargeTime)
                currentSpRechargeTime++;
            else
            {
                spUsed = false;
            }
        }
    }

    void SPRecover()
    {
        if(!spUsed && currentSp < sp)
        {
            currentSp += spIncreaseSpeed;
        }
    }

    void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다.");
    }

    void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("목마름 수치가 0이 되었습니다.");
    }

    void GaugeUpdate()
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[SP].fillAmount = (float)currentSp / sp;
        images_Gauge[DP].fillAmount = (float)currentDp / dp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
        images_Gauge[STATISFY].fillAmount = (float)currentSatisfy / satisfy;
    }

    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)
            currentHp += _count;
        else
            currentHp = hp;
    }

    public void DecreaseHP(int _count)
    {
        if(currentDp > 0)
        {
            DecreaseDP(_count);
            return;
        }
        currentHp -= _count;

        if (currentHp <= 0)
            Debug.Log("캐릭터의 hp가 0이 되었습니다!!");
    }

    public void IncreaseDP(int _count)
    {
        if (currentDp + _count < dp)
            currentDp += _count;
        else
            currentDp = dp;
    }

    public void DecreaseDP(int _count)
    {
        currentDp -= _count;

        if (currentDp <= 0)
            Debug.Log("캐릭터의 dp가 0이 되었습니다!!");
    }

    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry)
            currentHungry += _count;
        else
            currentHungry = hungry;
    }

    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
            currentHungry = 0;
        else
            currentHungry -= _count;
    }

    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }

    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= _count;
    }

    public void DecreaseStamina(int _count)
    {
        spUsed = true;
        currentSpRechargeTime = 0;

        if (currentSp - _count > 0)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    public void IncreaseSP(int _count)
    {
        sp += _count;
        Debug.Log("최대 sp가 증가했습니다.");
    }

    public int GetCurrentSP()
    {
        return currentSp;
    }
}
