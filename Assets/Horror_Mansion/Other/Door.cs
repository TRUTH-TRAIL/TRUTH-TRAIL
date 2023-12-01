using UnityEngine;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    bool trig, open;
    public bool Open { get { return open; } set { open = value; } }
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    private Quaternion defaultRot;
    private Quaternion openRot;
    public Text txt;
    public Transform player;
    private Curses curse;
    public GameObject Key;

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
        curse = GameObject.Find("CurseManager").GetComponent<Curses>();
        //txt = GameObject.FindObjectOfType<Text>();
//        txt = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        sound = GameObject.Find("Sound"); //우정추가
    }

    void Update()
    {
        if (open)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * smooth);
        }

        if (Input.GetMouseButtonDown(0) && trig)
        {
            if (Vector3.Dot(transform.right, player.position - transform.position) > 0)
            {
                
                openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
                //Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.Find("Key(Clone)").transform.gameObject.name);
                if(Key.activeSelf){
                    Key.SetActive(false);
                }
                /*if(GameObject.FindGameObjectWithTag("Player").transform.Find("Key(Clone)").transform.gameObject.activeSelf){
                    GameObject.FindGameObjectWithTag("Player").transform.Find("Key(Clone)").transform.gameObject.SetActive(false);
                }*/
            }
            else
            {
                openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y - DoorOpenAngle, defaultRot.eulerAngles.z);
            }
            open = !open;
            if (curse.activeCurse)
            {
                if(curse.curseKey == 3 && open)
                {
                    curse.doorCurseCount++;
                    if(curse.doorCurseCount == 2)
                    {
                        curse.die = true;
                    }
                }
                if(curse.curseKey == 14)
                {
                    curse.die = true;
                }
            }
        }

        if (trig)
        {
            if (open)
            {
                txt.text = "문 닫기(클릭)";
            }
            else
            {
                txt.text = "문 열기(클릭)";
            }
        }
    }

    public void OpenDoor(Transform interactor)
    {
        if (Vector3.Dot(transform.right, interactor.position - transform.position) > 0)
        {
            openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
        }
        else
        {
            openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y - DoorOpenAngle, defaultRot.eulerAngles.z);
        }
        open = !open;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (!open)
            {
                txt.text = "문 닫기(클릭)";
            }
            else
            {
                txt.text = "문 열기(클릭)";
            }
            trig = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            txt.text = " ";
            trig = false;
        }
    }
}
