using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public bool blink;
    // ���ǵ� ���� ����
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float slowSpeed;

    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameObject inventoryUI;

    // ���� ����
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;
    private bool isSlow = false;


    // �ɾ��� �� �󸶳� ������ �����ϴ� ����.
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    // �� ���� ����
    private CapsuleCollider capsuleCollider;


    // �ΰ���
    [SerializeField]
    private float lookSensitivity;


    // ī�޶� �Ѱ�
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;


    //�ʿ��� ������Ʈ
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera_Player;
    private Rigidbody myRigid;


    public float mouseXPos;
    public float mouseYPos;

    private Curses curse;

    // Use this for initialization
    void Start()
    {
        blink = true;
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        // �ʱ�ȭ.
        originPosY = virtualCamera_Player.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        curse = GameObject.Find("CurseManager").GetComponent<Curses>();
    }




    // Update is called once per frame
    void Update()
    {

        IsGround();
        TryJump();
        TrySlow();
        TryRun();
        //TryCrouch();
        if(Time.timeScale != 0)
        {
            if(blink)
                Move();
            if(!inventoryUI.activeSelf)
            {
                CharacterRotation();
                CameraRotation();
            }
            
        }
        

    }

    // �ɱ� �õ�
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }


    // �ɱ� ����
    private void Crouch()
    {
        isCrouch = !isCrouch;

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

    // �ε巯�� ���� ����.
    IEnumerator CrouchCoroutine()
    {

        float _posY = virtualCamera_Player.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            virtualCamera_Player.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        virtualCamera_Player.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
    }

   

    // ���� üũ.
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }


    // ���� �õ�
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }


    // ����
    private void Jump()
    {

        myRigid.velocity = transform.up * jumpForce;
    }


    // �޸��� �õ�
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    // �޸��� ����
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }


    // �޸��� ���?
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }


    private void TrySlow()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            WalkSlow();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            WalkSlowCancel();
        }
    }
    private void WalkSlow()
    {
        if (curse.activeCurse && curse.curseKey == 12)
        {
            curse.die = true;
        }
            isSlow = true;
        applySpeed = slowSpeed;
    }

    private void WalkSlowCancel()
    {
        isSlow = false;
        applySpeed = walkSpeed;
    }



    // ������ ����
    private void Move()
    {
        
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        bool isMoving = _moveDirX !=0 || _moveDirZ !=0;
        if (curse.activeCurse)
        {
            if(curse.curseKey == 11 && isMoving)
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    curse.die = true;
                }
            }
            if(curse.curseKey ==2 && _moveDirX != 0)
            {
                curse.die = true;
            }
            if(curse.curseKey == 8 && _moveDirZ == 1.0f){
                curse.die = true;
            }
            
        }
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // �¿� ĳ���� ȸ��
    private void CharacterRotation()
    {

        float _yRotation = Input.GetAxisRaw("Mouse X");
        mouseXPos = _yRotation;
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }



    // ���� ī�޶� ȸ��
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        mouseYPos = _xRotation;
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        if(curse.activeCurse && curse.curseKey==19){
            if(currentCameraRotationX<=-60.0f){
                curse.die = true;
            }
        }
        virtualCamera_Player.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }



}
