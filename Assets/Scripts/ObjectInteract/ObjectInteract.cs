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
        Poster,

    }
    private bool isBookMoving = false;
    private bool isDrawerMoving = false;
    private bool isFabricFading = false;
    private bool isFrameRotating = false;

    //서랍 문관리
    [SerializeField] ObjectDetector objectDetector;
    [SerializeField] float smooth = 2.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool isRotating;

    //단서 관리
    private int clueTextIndex = 0;
    [SerializeField] Curses curse;
    [SerializeField] Text[] text;
    [SerializeField] Text curseText;

    //책 관리
    private float slideSpeed = 2f;
    //Poster 관리
    private int cnt = 0;
    private bool[] getClue = new bool[15]; //단서 습득 유무
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
                // 추가 케이스를 여기에 작성합니다.
            case ObjectType.Poster:
                HandlePoster(target);
                break;
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
        int RandomInt = Random.Range(0, 5);
        RandomInt = 0;//임시 저주만 발생
        Debug.Log(RandomInt);
        if (RandomInt!=0 || curse.activeCurse)
        {
            text[clueTextIndex].text = clue.GetComponent<MemoScript>().memoData;
            getClue[clue.GetComponent<MemoScript>().key] = true; // 이부분은 추후 GameManager를 통해 관리
            text[clueTextIndex].gameObject.SetActive(true);
            clueTextIndex++;
        }
        else
        {
            curseText.gameObject.SetActive(true);
            curseText.text = curse.ActiveCurse();
        }
        
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
        if (curse.activeCurse && curse.curseKey == 18)
        {
            curse.die = true;
        }
        if (!isBookMoving)
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
        if (curse.activeCurse && curse.curseKey == 17)
        {
            curse.die = true;
        }
        if (!isDrawerMoving)
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

        float fadeDuration = 0.7f; // 천이 사라지는데 걸리는 시간
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float progress = elapsedTime / fadeDuration;

            fabricMeshRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, progress));
            //왜 안투명해질까...?
            target.parent.position = Vector3.Lerp(originalPosition, targetPosition, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(target.parent.gameObject);
        isFabricFading = false;
    }


    private void HandleFrame(Transform target)
    {
        if (curse.activeCurse && curse.curseKey == 16)
        {
            curse.die = true;
        }
        if (!isFrameRotating)
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

    private void HandlePoster(Transform target)
    {
        
        if(target.gameObject.name=="poster_A"){
            cnt++;
            Debug.Log("Alley Poster");
            //inventory 포스터 부분 업데이트
            //추후 inventory script에서 포스터 선택 시 포스터 내용 보이도록 수정
        }else if(target.gameObject.name=="poster_L"){
            Debug.Log("Light Poster");
            cnt++;
        }else if(target.gameObject.name=="poster_S"){
            Debug.Log("Special paper Poster");
            cnt++;
        }
        Destroy(target.gameObject);

        if(cnt==3){
            StartCoroutine(GoCellarText());
        }
    
    }

    IEnumerator GoCellarText()
    {
        GameObject Text = GameObject.Find("Canvas").transform.Find("tuText").gameObject;
        Text.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        Text.SetActive(false);
    }
}
