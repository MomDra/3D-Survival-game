using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField]
    string animalName; // 동물의 이름
    [SerializeField] int hp; // 동물의 체력

    [SerializeField]
    float walkSpeed; // 걷기 스피드
    [SerializeField]
    float runSpeed; // 뛰기 스피드
    float applySpeed;

    Vector3 direction; // 방향

    // 상태변수
    bool isWalking; // 걷는지 안 걷는지 판별
    bool isAction; // 행동중인지 아닌지 판별
    bool isRunning; // 뛰는지 판별
    bool isDead; // 죽었는지 판별

    [SerializeField]
    float walkTime; // 걷기 시간
    [SerializeField]
    float waitTime; // 대기 시간
    [SerializeField]
    float runTime; // 뛰기 시간
    float currentTime;

    // 필요한 컴포넌트
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody rigid;
    [SerializeField] BoxCollider boxCol;
    AudioSource theAudio;
    [SerializeField] AudioClip[] sound_pig_Normal;
    [SerializeField] AudioClip sound_pig_Hurt;
    [SerializeField] AudioClip sound_pig_Dead;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            ElapseTime();
            Move();
            Rotation();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
        }
    }

    void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                ReSet();
                RandomAction();
            }
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isAction = true;
        isRunning = false;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
    }

    void RandomAction()
    {
        RandomSound();

        isAction = true;

        int _random = Random.Range(0, 4);

        if(_random == 0)
        {
            Wait();
        }
        else if(_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }
        else if (_random == 3)
        {
            TryWalk();
        }
    }

    void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }

    void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }

    void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        applySpeed = walkSpeed;
        Debug.Log("걷기");
    }

    public void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = walkSpeed;
        anim.SetBool("Running", isRunning);
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(sound_pig_Hurt);
            anim.SetTrigger("Hunt");
            Run(_targetPos);
        }
    }

    void Dead()
    {
        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }

    void RandomSound()
    {
        int _random = Random.Range(0, 3); // 일상 사운드 3개
        PlaySE(sound_pig_Normal[_random]);
    }

    void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
