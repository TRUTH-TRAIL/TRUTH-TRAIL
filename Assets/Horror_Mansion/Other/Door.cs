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

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
        //txt = GameObject.FindObjectOfType<Text>();
//        txt = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                if(GameObject.Find("Player").transform.GetChild(1).GetChild(0).transform.gameObject.activeSelf){
                    GameObject.Find("Player").transform.GetChild(1).GetChild(0).transform.gameObject.SetActive(false);
                }
            }
            else
            {
                openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y - DoorOpenAngle, defaultRot.eulerAngles.z);
            }
            open = !open;
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
