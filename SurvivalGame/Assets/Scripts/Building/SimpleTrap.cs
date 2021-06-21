using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{
    Rigidbody[] rigid;
    [SerializeField]
    GameObject go_Meat;

    [SerializeField]
    int damage;

    bool isActivated;

    AudioSource theAudio;
    [SerializeField]
    AudioClip sound_Activate;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentsInChildren<Rigidbody>();
        theAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            if(other.transform.tag != "Untagged")
            {
                isActivated = true;
                theAudio.clip = sound_Activate;
                theAudio.Play();

                Destroy(go_Meat); // 고기 제거

                for(int i = 0; i < rigid.Length; i++)
                {
                    rigid[i].useGravity = true;
                    rigid[i].isKinematic = false;
                }

                if(other.transform.name == "Player")
                {
                    FindObjectOfType<StatusController>().DecreaseHP(damage);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
