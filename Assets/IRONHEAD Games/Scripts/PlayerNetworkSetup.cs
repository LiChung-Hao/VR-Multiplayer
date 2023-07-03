using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject LocalVRRigPlayerGameObject;

    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //it's local
            LocalVRRigPlayerGameObject.SetActive(true);
            SetLayerRecursively(AvatarHeadGameObject, 6);
            SetLayerRecursively(AvatarBodyGameObject, 7);

            TeleportationArea[] teleportationAreas=GameObject.FindObjectsOfType<TeleportationArea>();

            if (teleportationAreas.Length > 0)
            {
                Debug.Log("Found " + teleportationAreas.Length + " teleport areas.");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider=LocalVRRigPlayerGameObject.GetComponent<TeleportationProvider>();
                }
            }

        }
        else
        {
            //it's other player(remote)
            LocalVRRigPlayerGameObject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameObject, 0);
            SetLayerRecursively(AvatarBodyGameObject, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
