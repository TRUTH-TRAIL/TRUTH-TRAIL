using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] ObjectDetector objectDetector;
    [SerializeField] float smooth = 2.0f;
    private Vector3 defaultRot;
    private Vector3 openRot;
    private bool isRotating;
    private void Awake()
    {
        objectDetector.raycastEvent.AddListener(OnHit);
        isRotating = false;
    }

    private void OnHit(Transform target)
    {
        if (target.CompareTag("Door") && !isRotating)
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
}
