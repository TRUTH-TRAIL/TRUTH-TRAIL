using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Stop stop;

    // ���ǵ� ���� ����
    [SerializeField]
    private float walkSpeed;
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
    private Camera theCamera;

    private Rigidbody myRigid;


    public float mouseXPos;
    public float mouseYPos;

    // Use this for initialization
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        // �ʱ�ȭ.
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;

        stop = this.GetComponent<Stop>();
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

        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
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


    // �޸��� ���
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

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed * stop.PlayertimeScale;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // �¿� ĳ���� ȸ��
    private void CharacterRotation()
    {

        float _yRotation = Input.GetAxisRaw("Mouse X");
        mouseXPos = _yRotation;
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity * stop.PlayertimeScale;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }



    // ���� ī�޶� ȸ��
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        mouseYPos = _xRotation;
        float _cameraRotationX = _xRotation * lookSensitivity * stop.PlayertimeScale;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
