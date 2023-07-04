using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    PhotonView m_PhotonView;

    Rigidbody m_Rigidbody;
    bool m_isBeingHeld = false;
    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isBeingHeld)
        {
            //gun is now being held by someone
            m_Rigidbody.isKinematic = true;
            gameObject.layer = 11;
        }
        else
        {
            m_Rigidbody.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    private void TransferOwnership()
    {
        m_PhotonView.RequestOwnership();
    }

    public void OnSelectEntered()
    {
        Debug.Log("Grabbed");
        m_PhotonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered); //work on everyone in the room including people who join later

        if (m_PhotonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("No need to transfer ownership. Already mine");
        }
        else 
        {
            TransferOwnership();
        }
    }
    public void OnSelectExited()
    {
        Debug.Log("Released");
        m_PhotonView.RPC("EndNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_PhotonView) //only this script attached target object will be called 
        {
            return;
        }

        Debug.Log("Ownership requested for: " + targetView.name + " from: " + requestingPlayer.NickName);
        targetView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Ownership transferred to: " + targetView.name + " from: " + previousOwner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {

    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        m_isBeingHeld = true;
    }

    [PunRPC]
    public void EndNetworkGrabbing()
    {
        m_isBeingHeld = false;
    }
}
