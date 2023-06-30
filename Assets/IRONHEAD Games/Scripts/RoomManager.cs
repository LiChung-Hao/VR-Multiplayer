using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() 
        { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };

        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    }
    public void OnEnterButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };

        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }
    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with a name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + 
            " has joined to room: " + PhotonNetwork.CurrentRoom.Name + 
            ". Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with map: " + (string)mapType);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName+" has joined the room. Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);

        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 20;

        string[] roomProsInLobby = { "map" };
        //We have 2 different maps
        //1. Outdoor = outdoor
        //2. School = school

        ExitGames.Client.Photon.Hashtable customRoomProperties= 
            new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };

        roomOption.CustomRoomPropertiesForLobby = roomProsInLobby;
        roomOption.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOption);


    }
    #endregion
}
