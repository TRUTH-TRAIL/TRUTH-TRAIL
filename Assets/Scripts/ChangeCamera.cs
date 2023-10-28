using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChangeCamera : MonoBehaviour
{
    public CinemachineVirtualCameraBase virtualCamera_Alley;
    public CinemachineVirtualCameraBase virtualCamera_Player;
    public Transform mainCameraTransform;

    private int changeID = 0;

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(changeID == 0){
                changeID ++;
                SwitchToVirtualCamera();
            }else{
                changeID = 0;
                SwitchToMainCamera();
            }
        }
    } 
    public void SwitchToVirtualCamera()
    {
        CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0.5f);
        CinemachineBlendDefinition hardOutBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0.0f);

        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        cinemachineBrain.m_DefaultBlend = hardOutBlend;
        

        virtualCamera_Alley.VirtualCameraGameObject.SetActive(true);

    }

    public void SwitchToMainCamera()
    {
        Transform thisTransform = transform;
        Transform playerTransform = thisTransform.parent;
        Vector3 playerPosition = playerTransform.position;
        playerPosition.y += 0.4f;
        mainCameraTransform.position = playerPosition;
        virtualCamera_Alley.VirtualCameraGameObject.SetActive(false);
        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0.0f);
    }
}

