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
        Candle,
        Skull,
        Cross,
        Decal,
        Lighter,
        Basic,
    }
    private bool isBookMoving = false;
    private bool isDrawerMoving = false;
    private bool isFabricFading = false;
    private bool isFrameRotating = false;

    //ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
    [SerializeField] ObjectDetector objectDetector;
    [SerializeField] float smooth = 2.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool isRotating;

    //´Ü¼­ °ü¸®
    private int clueTextIndex = 0;
    [SerializeField] Curses curse;
    [SerializeField] Text[] text;
    [SerializeField] Text curseText;

    //Ã¥ ï¿½ï¿½ï¿½ï¿½
    private float slideSpeed = 2f;
    //Poster ï¿½ï¿½ï¿½ï¿½
    private int cnt = 0;
    private bool[] getClue = new bool[15]; //´Ü¼­ ½Àµæ À¯¹«

    //
    private MemoInteract memoInteract;
    private GameObject Player;
    private CluePlacement cluePlacement;
    [SerializeField] private GameObject Blood_Decals_05;

    [SerializeField]
    Transform itemGroup;
    private int candleNum = 3;//¿ø·¡´Â 0ÀÌ´Ù.@@@@@@@@@@@@@@@@@@@@@@
    private Inventory inventory;

    //Åð¸¶ ¿ÀºêÁ§Æ® »ý¼º
    private GameObject fSkull;
    private GameObject fPoster;
    private GameObject fCross;
    private List<GameObject> fCandle = new List<GameObject>();
    private int ignite = 0;
    private void Awake()
    {
        objectDetector.raycastEvent.AddListener(OnHit);
        isRotating = false;
    }
    private void Start()
    {
        memoInteract = gameObject.GetComponent<MemoInteract>();
        cluePlacement = gameObject.GetComponent<CluePlacement>();
        Player = GameObject.FindWithTag("Player");
        inventory = gameObject.GetComponent<Inventory>();
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
                // ï¿½ß°ï¿½ ï¿½ï¿½ï¿½Ì½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½â¿¡ ï¿½Û¼ï¿½ï¿½Õ´Ï´ï¿½.
            case ObjectType.Poster:
                HandlePoster(target);
                break;
            case ObjectType.Candle:
                HandleCandle(target);
                break;
            case ObjectType.Skull:
                HandleSkull(target);
                break;
            case ObjectType.Cross:
                HandleCross(target);
                break;
            case ObjectType.Lighter:
                HandleLighter(target);
                break;
            case ObjectType.Decal:
                HandleDecal(target);
                break;
            case ObjectType.Basic:
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
        ClueUpdate(target.gameObject);
    }
    private void ClueUpdate(GameObject clue)
    {
        int RandomInt = Random.Range(0, 5);
        //RandomInt = 0;//ÀÓ½Ã ÀúÁÖ¸¸ ¹ß»ý
        Debug.Log(RandomInt);
        
        if (RandomInt!=0 || curse.activeCurse)
        {
            MemoScript memoscript = clue.GetComponent<MemoScript>();
            text[clueTextIndex].text = memoscript.memoData;
            getClue[memoscript.key] = true; // ÀÌºÎºÐÀº ÃßÈÄ GameManager¸¦ ÅëÇØ °ü¸®
            text[clueTextIndex].gameObject.SetActive(true);
            clueTextIndex++;
            Debug.Log(memoscript.key + "AAA");
            memoInteract.ObjectAppear(memoscript.key);
            Destroy(clue);
            if(clueTextIndex>=10)
                Blood_Decals_05.SetActive(true);
        }
        else
        {
            curseText.gameObject.SetActive(true);
            curseText.text = curse.ActiveCurse();
            cluePlacement.RelocationClue(clue);
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

        float fadeDuration = 0.7f; // Ãµï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Âµï¿? ï¿½É¸ï¿½ï¿½ï¿½ ï¿½Ã°ï¿½
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float progress = elapsedTime / fadeDuration;

            fabricMeshRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, progress));
            //ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½...?
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
            //inventory ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Îºï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
            //ï¿½ï¿½ï¿½ï¿½ inventory scriptï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ìµï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
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
    private void HandleCandle(Transform target)
    {
        if(clueTextIndex>=10&&fCandle==null){
            Destroy(target.gameObject);
            itemGroup.GetChild(2+candleNum).gameObject.SetActive(true);
            candleNum++;
        }else{
            if(Player.transform.GetChild(3).gameObject.activeSelf)
            {
                if (curse.curseKey < 20 && curse.activeCurse)
                {
                    curse.ClaerCurse();
                }
            }
        }

        if(fCandle!=null){
            if(inventory.handLighter.activeSelf){
                target.parent.GetChild(0).gameObject.SetActive(true);
                ignite++;
            }
            if(ignite>=3){
                inventory.handLighter.SetActive(false);
                itemGroup.GetChild(5).gameObject.SetActive(false);
            }
        }
    }

    private void HandleSkull(Transform target){
        if(fSkull==null){
            Destroy(target.parent.gameObject);
            itemGroup.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void HandleCross(Transform target){
        if(fCross==null){
            Destroy(target.gameObject);
            itemGroup.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void HandleLighter(Transform target){
        Destroy(target.parent.gameObject);
        itemGroup.GetChild(5).gameObject.SetActive(true);
    }

    private void HandleDecal(Transform target){
        if(inventory.handSkull.activeSelf){
            fSkull = Instantiate(inventory.handSkull, new Vector3(274.414398f,6.00546074f,260.990967f),Quaternion.identity);
            inventory.handSkull.SetActive(false);
            itemGroup.GetChild(0).gameObject.SetActive(false);
            fSkull.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if(inventory.handCross.activeSelf){
            fCross = Instantiate(inventory.handCross, new Vector3(274.987f,6.08400011f,260.990967f),Quaternion.identity);
            inventory.handCross.SetActive(false);
            itemGroup.GetChild(1).gameObject.SetActive(false);
            fCross.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if(inventory.handCandle.activeSelf){
            if(candleNum==3){
                fCandle.Add(Instantiate(inventory.handCandle, new Vector3(273.636993f,6.09499979f,260.990967f),Quaternion.identity));
            }else if(candleNum==2){
                fCandle.Add(Instantiate(inventory.handCandle, new Vector3(274.951996f,6.09299994f,260.239014f),Quaternion.identity));
            }else{
                fCandle.Add(Instantiate(inventory.handCandle, new Vector3(274.951996f,6.09299994f,261.751007f),Quaternion.identity));
            }
            
            inventory.handCandle.SetActive(false);
            itemGroup.GetChild(1+candleNum).gameObject.SetActive(false);
            candleNum--;
            Debug.Log(candleNum);
            fCandle[2-candleNum].transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if(inventory.handSpecialPaper.activeSelf){
            int fcnt = 0;
            for(int i=0; i<itemGroup.childCount;i++){
                if(itemGroup.GetChild(i).gameObject.activeSelf){
                    fcnt++;
                }
            }
            if(fcnt==0&&ignite>=3){
                //ending
            }
        }
    }
    

}