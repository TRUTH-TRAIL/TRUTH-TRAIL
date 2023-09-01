using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ObjectInteract : MonoBehaviour
{
    public enum ObjectType
    {
        Door,
        Clue,
        Flash,
        Battery,
        Book,
        Drawer,
        Fabric,
        Frame,
    }
    private bool isBookMoving = false;
    private bool isDrawerMoving = false;
    private bool isFabricFading = false;
    private bool isFrameRotating = false;

    //���� ������
    [SerializeField] ObjectDetector objectDetector;
    [SerializeField] float smooth = 2.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool isRotating;
    private int clueTextIndex = 0;
    [SerializeField] Text[] text;

    //å ����
    private float slideSpeed = 2f;

    private bool[] getClue = new bool[15]; //�ܼ� ���� ����
    private void Awake()
    {
        objectDetector.raycastEvent.AddListener(OnHit);
        isRotating = false;
    }
    private void Start()
    {

    }

    private void OnHit(Transform target)
    {
        ObjectTypeController controller = target.GetComponent<ObjectTypeController>();
        if (controller == null) return;

        ObjectType objectType = controller.objectType;

        switch (objectType)
        {
            case ObjectType.Door:
                HandleDoor(target);
                break;
            case ObjectType.Clue:
                HandleClue(target);
                break;
            case ObjectType.Flash:
                HandleFlash(target);
                break;
            case ObjectType.Battery:
                HandleBattery(target);
                break;
            case ObjectType.Book:
                HandleBook(target);
                break;
            case ObjectType.Drawer:
                HandleDrawer(target);
                break;
            case ObjectType.Fabric:
                HandleFabric(target);
                break;
            case ObjectType.Frame:
                HandleFrame(target);
                break;
                // �߰� ���̽��� ���⿡ �ۼ��մϴ�.
        }
    }

    private void HandleDoor(Transform target)
    {
        if (!isRotating)
        {
            DoorFSM door = target.GetComponent<DoorFSM>();
            if (!door.isOpen)
            {
                StartCoroutine(DoorRotate(target, door.isOpen, door.openAngle));
                door.isOpen = true;
            }
            else
            {
                StartCoroutine(DoorRotate(target, door.isOpen, door.openAngle));
                door.isOpen = false;
            }
        }
    }
    private IEnumerator DoorRotate(Transform target, bool isOpen, float openAngle)
    {
        isRotating = true;
        float timer = 0.0f;
        defaultRot = target.eulerAngles;
        if (!isOpen)
        {
            openRot = new Vector3(defaultRot.x, defaultRot.y + openAngle, defaultRot.z);
        }
        else
        {
            openRot = new Vector3(defaultRot.x, defaultRot.y - openAngle, defaultRot.z);
        }

        while (timer <= 1.0f)
        {
            timer += Time.deltaTime * smooth;
            target.eulerAngles = Vector3.Lerp(defaultRot, openRot, timer);
            yield return null;
        }
        isRotating = false;
    }


    private void HandleClue(Transform target)
    {
        Destroy(target.gameObject);
        ClueUpdate(target.gameObject);
    }
    private void ClueUpdate(GameObject clue)
    {
        text[clueTextIndex].text = clue.GetComponent<MemoScript>().memoData;
        getClue[clue.GetComponent<MemoScript>().key] = true; // �̺κ��� ���� GameManager�� ���� ����
        text[clueTextIndex].gameObject.SetActive(true);
        clueTextIndex++;
    }

    private void HandleFlash(Transform target)
    {
        GameObject.FindWithTag("Player").GetComponent<FlashLight>().getFlash = true;
        Destroy(target.parent.gameObject);


    }

    private void HandleBattery(Transform target)
    {
        Destroy(target.gameObject);
        GameObject.FindWithTag("Player").GetComponent<FlashLight>().UpdateBattery(false);
    }

    private void HandleBook(Transform target)
    {
        if(!isBookMoving)
            StartCoroutine(MoveBook(target));  
    }
    private IEnumerator MoveBook(Transform target)
    {
        isBookMoving = true;
        BookFSM book = target.GetComponent<BookFSM>();
        Vector3 originalPosition = target.position;
        Vector3 targetPosition = originalPosition;
        float slideDistance = book.distance;

        switch (book.axis)
        {
            case 0:
                if (book.loc)
                    targetPosition += new Vector3(slideDistance, 0, 0);
                else
                    targetPosition -= new Vector3(slideDistance, 0, 0);
                break;
            case 1:
                if (book.loc)
                    targetPosition += new Vector3(0, slideDistance, 0);
                else
                    targetPosition -= new Vector3(0, slideDistance, 0);
                break;
            case 2:
                if (book.loc)
                    targetPosition += new Vector3(0, 0, slideDistance);
                else
                    targetPosition -= new Vector3(0, 0, slideDistance);
                break;
        }
        book.loc = !book.loc;

        float timer = 0.0f;
        while(timer <= 1.0f)
        {
            timer += Time.deltaTime * slideSpeed;
            target.position = Vector3.Lerp(originalPosition, targetPosition, timer);
            yield return null;
        }
        target.position = targetPosition;
        isBookMoving = false;
    }


    private void HandleDrawer(Transform target)
    {
        if(!isDrawerMoving)
            StartCoroutine(MoveDrawer(target));
    }

    private IEnumerator MoveDrawer(Transform target)
    {
        isDrawerMoving = true;
        DrawerFSM drawer = target.GetComponent<DrawerFSM>();
        Vector3 originalPosition = target.localPosition;
        Vector3 targetPosition = originalPosition;
        float slideDistance = drawer.distance;

        if(drawer.isOpen)
            targetPosition -= new Vector3(slideDistance, 0, 0);
        else
            targetPosition += new Vector3(slideDistance, 0, 0);

        drawer.isOpen = !drawer.isOpen;

        float timer = 0.0f;
        while (timer <= 1.0f)
        {
            timer += Time.deltaTime * slideSpeed;
            target.localPosition = Vector3.Lerp(originalPosition, targetPosition, timer);
            yield return null;
        }
        target.localPosition = targetPosition;
        isDrawerMoving = false;
    }

    private void HandleFabric(Transform target)
    {
        if(!isFabricFading)
            StartCoroutine(FadeAndMoveFabric(target));
    }

    private IEnumerator FadeAndMoveFabric(Transform target)
    {
        isFabricFading = true;
        float moveDistance = 1f;
        Vector3 originalPosition = target.parent.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);

        MeshRenderer fabricMeshRenderer = target.GetComponent<MeshRenderer>();
        Color originalColor = fabricMeshRenderer.material.color;

        float fadeDuration = 0.7f; // õ�� ������µ� �ɸ��� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float progress = elapsedTime / fadeDuration;

            fabricMeshRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, progress));
            //�� ������������...?
            target.parent.position = Vector3.Lerp(originalPosition, targetPosition, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(target.parent.gameObject);
        isFabricFading = false;
    }


    private void HandleFrame(Transform target)
    {
        if(!isFrameRotating)
            StartCoroutine(FrameRotate(target));

    }
    private IEnumerator FrameRotate(Transform target)
    {
        isFrameRotating = true;
        float duration = 1.0f; 
        float timer = 0.0f;
        float maxAngle = 10.0f; 

        Vector3 originalRotation = target.eulerAngles;

        Transform childObject = null;
        Vector3 childOriginalPosition = Vector3.zero;
        Vector3 childTargetPosition = Vector3.zero;

        if (target.childCount > 0)
        {
            childObject = target.GetChild(0); 
            childOriginalPosition = childObject.position;
            childTargetPosition = childOriginalPosition + new Vector3(0, -1, 0);
        }

        while (timer < duration)
        {
            float angle = Mathf.Sin(Time.time * 15) * maxAngle;  
            target.eulerAngles = originalRotation + new Vector3(angle, 0, 0);

            if (childObject != null)
            {
                float progress = timer / duration;
                childObject.position = Vector3.Lerp(childOriginalPosition, childTargetPosition, progress);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        target.eulerAngles = new Vector3(0,originalRotation.y,originalRotation.z);

        if (childObject != null)
        {
            childObject.position = childTargetPosition;
            childObject.SetParent(null);
        }

        isFrameRotating = false;
    }
}
