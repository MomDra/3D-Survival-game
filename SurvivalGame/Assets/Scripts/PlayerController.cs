using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���ǵ� ���� ����
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float runSpeed;
    [SerializeField]
    float crouchSpeed;
    float applySpeed;

    [SerializeField]
    float jumpForce;

    // ���� ����
    bool isWalk;
    bool isRun;
    bool isGround = true;
    bool isCrouch;

    // ������ üũ ����
    Vector3 lastPos;

    // �ɾ��� �� �󸶳� ������ �����ϴ� ����
    [SerializeField]
    float crouchPosY;
    float originPosY;
    float applyCrouchPosY;


    // �ΰ���
    [SerializeField]
    float lookSensitivity;

    // ī�޶� �Ѱ�
    [SerializeField]
    float cameraRotationLimit;
    float currentCameraRotationX;

    // �ʿ��� ������Ʈ
    [SerializeField]
    Camera theCamera;
    Rigidbody myRigid;
    CapsuleCollider capsuleCollider;
    GunController theGunController;
    Crosshair theCrosshair;
    StatusController theStatusController;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        theGunController = FindObjectOfType<GunController>();
        theCrosshair = FindObjectOfType<Crosshair>();
        theStatusController = FindObjectOfType<StatusController>();

        // �ʱ�ȭ
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.canPlayerMove)
        {
            IsGround();
            TryJump();
            TryRun();
            TryCrouch();
            Move();

            CameraRotation();
            CharacterRotation();      
        }
    }

    private void FixedUpdate()
    {
        MoveCheck();
    }

    // �ɱ� �õ�
    void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
           Crouch();
        }
    }

    // �ɱ� ����
    void Crouch()
    {
        isCrouch = !isCrouch;
        theCrosshair.CrouchAnimation(isCrouch);

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    // �ε巯�� �ɱ� ���� ����
    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15) break;
            yield return null;
        }

        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    // ���� üũ
    void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrosshair.JumpAnimation(!isGround);
    }

    // ���� �õ�
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && theStatusController.GetCurrentSP() > 0)
        {
            Jump();
        }
    }

    // ���� ����
    void Jump()
    {
        if (isCrouch)
            Crouch();
        myRigid.velocity = transform.up * jumpForce;
        theStatusController.DecreaseStamina(50);
    }

    // �޸��� �õ�
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSP() <= 0)
        {
            RunningCancel();
        }
    }

    // �޸��� ����
    void Running()
    {
        if (isCrouch)
            Crouch();

        theGunController.CancelFineSight();

        isRun = true;
        theCrosshair.RunAnimation(isRun);
        theStatusController.DecreaseStamina(1);
        applySpeed = runSpeed;
    }

    // �޸��� ���
    void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunAnimation(isRun);
        applySpeed = walkSpeed;
    }

    // ������ ����
    void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    void MoveCheck()
    {
        if (!isRun && !isCrouch && isGround)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;

            theCrosshair.WalkAnimation(isWalk);
            lastPos = transform.position;
        }
        else
        {
            isWalk = false;
            theCrosshair.WalkAnimation(isWalk);
        }
    }

    // ���� ī�޶� ȸ��
    void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

    // �¿� �÷��̾� ȸ��
    void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0, _yRotation, 0) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
