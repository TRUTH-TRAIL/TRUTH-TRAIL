using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Window,
        bathroom,
        coffin,
        cellarbook,
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
    public bool newClue = false;
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
    private int candleNum = 0;//¿ø·¡´Â 0ÀÌ´Ù.@@@@@@@@@@@@@@@@@@@@@@
    private Inventory inventory;

    //Åð¸¶ ¿ÀºêÁ§Æ® »ý¼º
    private GameObject fSkull;
    private GameObject fPoster;
    private GameObject fCross;
    private List<GameObject> fCandle = new List<GameObject>();
    private int ignite = 0;
    private AudioSource audioSource;
    private string retText;
    private bool isbath = false;
    private bool iscoffin = false;
    private bool iscellar = false;
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
        audioSource = GetComponent<AudioSource>();
        isbath = false;
        iscoffin = false;
        iscellar = false;
        newClue = false;
    }

    private void Update(){
        if(SceneManager.GetActiveScene().name == "GameScene_woo"){
            if(curse.activeTimer)
                curseText.text = retText + (curse.countTimer/60).ToString() + ":"+(curse.countTimer%60).ToString();   
        }
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
            case ObjectType.bathroom:
                HandleBathroom(target);
                break;
            case ObjectType.coffin:
                HandleCoffin(target);
                break;
            case ObjectType.cellarbook:
                HandleCellarbook(target);
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
        iscoffin=false;
    }


    private void HandleClue(Transform target)
    {
        ClueUpdate(target.gameObject);
        newClue = true;
    }
    private void ClueUpdate(GameObject clue)
    {
        int RandomInt = Random.Range(0, 5);
        RandomInt = 1;//ÀÓ½Ã ÀúÁÖ¸¸ ¹ß»ý
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
            if(memoscript.key==0)
                iscellar=true;
            if(memoscript.key==2)
                iscoffin=true;
            if(memoscript.key==3) 
                isbath=true;
            Destroy(clue);
            if(clueTextIndex>=10)
                Blood_Decals_05.SetActive(true);
        }
        else
        {
            curseText.gameObject.SetActive(true);
            retText = curse.ActiveCurse();
            cluePlacement.RelocationClue(clue);
        }
        
    }

    private void HandleFlash(Transform target)
    {
        GameObject.FindWithTag("Player").GetComponent<FlashLight>().getFlash = true;
        GameObject.FindWithTag("Player").transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        if(GameObject.Find("Flash_info").activeSelf)
            GameObject.Find("Flash_info").SetActive(false);
        Destroy(target.parent.gameObject);
    }

    private void HandleBattery(Transform target)
    {
        Destroy(target.gameObject);
        GameObject.FindWithTag("Player").GetComponent<FlashLight>().UpdateBattery(false);
    }

    private void HandleBook(Transform target)
    {
        if (curse.activeCurse && (curse.curseKey == 18||curse.curseKey == 7))
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
        if (curse.activeCurse && (curse.curseKey == 17 || curse.curseKey == 6))
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
        
        /*if(target.gameObject.name=="poster_A"){
            cnt++;
            Debug.Log("Alley Poster");
            //inventory ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Îºï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
            //ï¿½ï¿½ï¿½ï¿½ inventory scriptï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ìµï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        }else if(target.gameObject.name=="poster_L"){
            Debug.Log("Light Poster");
            cnt++;
        }else*/
        if(target.gameObject.name == "poster_S"){
            gameObject.GetComponent<Inventory>().enabled = true;
            //Debug.Log("Special paper Poster");
            //cnt++;
        }
        Destroy(target.gameObject);
        Destroy(GameObject.Find("SP_info"));
       /* if(cnt==3){
            StartCoroutine(GoCellarText());
        }*/
    
    }

   /* IEnumerator GoCellarText()
    {
        GameObject Text = GameObject.Find("Canvas").transform.Find("tuText").gameObject;
        Text.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        Text.SetActive(false);
    }*/
    private void HandleCandle(Transform target)
    {
        Debug.Log(clueTextIndex);
        Debug.Log(fCandle);
        //°¡Â¥ ´Ü¼­µµ Ãß°¡ÇÏ¸é ÀÌ Á¶°Çµµ Á» ¼öÁ¤ÇØ¾ß°Ú±º
        if(clueTextIndex>=10&&candleNum<3){
            Destroy(target.parent.gameObject);
            itemGroup.GetChild(2+candleNum).gameObject.SetActive(true);
            candleNum++;
        }else{
            if(Player.transform.GetChild(1).GetChild(5).gameObject.activeSelf && newClue)
            {
                if (curse.curseKey < 20 && curse.activeCurse)
                {
                    RawImage rawImage = Player.transform.GetChild(3).GetChild(0).GetComponent<RawImage>();
                    StartCoroutine(AlphaColor(rawImage));
                    curse.ClearCurse();
                    //audioSource.Play();
                }
                //°¡Â¥´Ü¼­ clear if¹® ÇÊ¿ä
                Player.transform.GetChild(1).GetChild(5).gameObject.SetActive(false);
                newClue = false;
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

    private IEnumerator AlphaColor(RawImage rawImage)
    {
        float duration = 1.0f; // Åõ¸íµµ º¯°æ¿¡ °É¸®´Â ÃÑ ½Ã°£
        float halfDuration = duration / 2.0f; // 0¿¡¼­ 1, ±×¸®°í 1¿¡¼­ 0À¸·Î º¯°æÇÏ´Â µ¥ °¢°¢ °É¸®´Â ½Ã°£
        float timer = 0.0f;

        // 0¿¡¼­ 1·Î ¾ËÆÄ°ª Áõ°¡
        while (timer < halfDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / halfDuration);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Àá½Ã 1·Î À¯Áö
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 1f);
        
        // 1¿¡¼­ 0À¸·Î ¾ËÆÄ°ª °¨¼Ò ½ÃÀÛ
        timer = 0.0f; // Å¸ÀÌ¸Ó Àç¼³Á¤
        while (timer < halfDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / halfDuration);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // ¸¶Áö¸·À¸·Î ¾ËÆÄ°ªÀ» 0À¸·Î ¼³Á¤
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
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
        Destroy(target.gameObject);
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
                LoadingScene.Instance.LoadScene("success");
            }
        }
    }
    
    private void HandleBathroom(Transform target)
    {
        if (isbath)
        {
            StartCoroutine(Delwater(target));
            isbath=false;
            target.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    private IEnumerator Delwater(Transform target)
    {
        Transform water = target.parent.GetChild(3);
        Vector3 startPosition = water.position;
        Vector3 startScale = water.localScale;
        
        Vector3 endPosition = startPosition - new Vector3(0, 0.11f, 0);
        Vector3 endScale = startScale - new Vector3(0.1f, 0, 0.1f);

        float timer = 0.0f;

        while (timer <= 1.0f)
        {
            timer += Time.deltaTime;

            water.position = Vector3.Lerp(startPosition, endPosition, timer);
            water.localScale = Vector3.Lerp(startScale, endScale, timer);

            yield return null;
        }

        Destroy(water.gameObject);
    }


    private void HandleCoffin(Transform target)
    {
        if (iscoffin)
        {
            StartCoroutine(rotateCoffin(target));
            iscoffin=false;
            target.parent.parent.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            target.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    private IEnumerator rotateCoffin(Transform target)
    {


        Transform coffin = target.parent;
        Quaternion startRotation = Quaternion.identity; 
        Quaternion endRotation = Quaternion.Euler(-60, 0, 0);

        float timer = 0.0f;

        while (timer <= 1.0f)
        {
            timer += Time.deltaTime;
            coffin.localRotation = Quaternion.Lerp(startRotation, endRotation, timer / 1.0f);
            
            yield return null;
        }
        timer = 0.0f;
        startRotation = endRotation;
        endRotation = Quaternion.Euler(-60, -9, 0);
        while (timer <= 0.5f)
        {
            timer += Time.deltaTime;
            coffin.localRotation = Quaternion.Lerp(startRotation, endRotation, timer / 0.5f);
            
            yield return null;
        }
    }

    private void HandleCellarbook(Transform target)
    {
        if (iscellar)
        {
            StartCoroutine(OpenCellar(target));
            iscellar=false;
        }
    }

    private IEnumerator OpenCellar(Transform target)
    {
        Transform book = target.parent;
        Transform bookcase = book.parent;
        Vector3 bookStartPosition = book.localPosition; 
        Vector3 bookEndPosition = bookStartPosition + new Vector3(0.2f, 0, 0);

        Vector3 bookcaseStartPosition = bookcase.localPosition; 
        Vector3 bookcaseEndPosition = bookcaseStartPosition + new Vector3(0, 0, 1.9f);

        float timer = 0.0f;

        while (timer <= 1.0f)
        {
            timer += Time.deltaTime;
            book.localPosition = Vector3.Lerp(bookStartPosition, bookEndPosition, timer / 1.0f);
            yield return null;
        }
        Destroy(book.gameObject);
        timer = 0.0f;
        while (timer <= 2.0f)
        {
            timer += Time.deltaTime;
            bookcase.localPosition = Vector3.Lerp(bookcaseStartPosition, bookcaseEndPosition, timer / 2.0f);
            yield return null;
        }
        
    }
}