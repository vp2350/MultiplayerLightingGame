using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    public static PhotonLobby lobby;
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;

    private void Awake()
    {
        lobby = this;
    }
    // Start is called before the first frame update
    void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectedScreen.SetActive(true);
        disconnectedScreen.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectedScreen.SetActive(false);
        disconnectedScreen.SetActive(true);
    }



    public void OnBattleButtonClicked()
    {
        //battleButton.SetActive(false);
        //cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random room but failed. There must be no open games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.multiplayerSetting.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

        Debug.Log("Tried to create a room but failed, there must already be a room with the same name");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        //cancelButton.SetActive(false);
        //battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
