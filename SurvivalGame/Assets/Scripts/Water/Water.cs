using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField]
    float waterDrag; // ¹°¼Ó Áß·Â
    float originDrag;

    [SerializeField]
    Color waterColor; // ¹°¼Ó »ö±ò
    [SerializeField]
    float waterFogDensity; // ¹° Å¹ÇÔ Á¤µµ

    [SerializeField]
    Color waterNightColor; // ¹ã »óÅÂÀÇ ¹°¼Ó »ö±ò
    [SerializeField]
    float waterNightFogDensity;

    Color originColor;
    float originFogDensity;

    [SerializeField]
    Color originNightColor;
    [SerializeField]
    float originNightFogDensity;

    [SerializeField]
    string sound_WaterOut;
    [SerializeField]
    string sound_WaterIn;
    [SerializeField]
    string sound_WaterBreathe;

    [SerializeField]
    float breatheTime;
    float currentBretheTime;

    [SerializeField]
    float totalOxygen;
    float currentOxygen;
    float temp;

    [SerializeField]
    GameObject go_BaseUI;
    [SerializeField]
    Text text_totalOxygen;
    [SerializeField]
    Text text_currentOxygen;
    [SerializeField]
    Image image_gauage;

    StatusController thePlayerStat;

    // Start is called before the first frame update
    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;

        originDrag = 0;
        thePlayerStat = FindObjectOfType<StatusController>();
        currentOxygen = totalOxygen;
        text_totalOxygen.text = totalOxygen.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isWater)
        {
            currentBretheTime += Time.deltaTime;
            if(currentBretheTime >= breatheTime)
            {
                SoundManager.instance.PlaySE(sound_WaterBreathe);
                currentBretheTime = 0;
            }
        }

        DecreaseOxygen();
    }

    void DecreaseOxygen()
    {
        if (GameManager.isWater)
        {
            currentOxygen -= Time.deltaTime;
            if (currentOxygen < 0) currentOxygen = 0;
            text_currentOxygen.text = Mathf.RoundToInt(currentOxygen).ToString();
            image_gauage.fillAmount = currentOxygen / totalOxygen;

            if(currentOxygen <= 0)
            {
                temp += Time.deltaTime;
                if(temp >= 1)
                {
                    thePlayerStat.DecreaseHP(1);
                    temp = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetWater(other);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GetOutWater(other);
        }
    }

    void GetWater(Collider _player)
    {
        currentOxygen = totalOxygen;
        SoundManager.instance.PlaySE(sound_WaterIn);
        go_BaseUI.SetActive(true);

        GameManager.isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;

        if (!GameManager.isNight)
        {
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterFogDensity;
        }
        else
        {
            RenderSettings.fogColor = waterNightColor;
            RenderSettings.fogDensity = waterNightFogDensity;
        }
        
    }

    void GetOutWater(Collider _player)
    {
        go_BaseUI.SetActive(false);
        SoundManager.instance.PlaySE(sound_WaterOut);
        if (GameManager.isWater)
        {
            GameManager.isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;

            if (!GameManager.isNight)
            {
                RenderSettings.fogColor = originColor;
                RenderSettings.fogDensity = originFogDensity;
            }
            else
            {
                RenderSettings.fogColor = originNightColor;
                RenderSettings.fogDensity = originNightFogDensity;
            }
        }
    }
}

