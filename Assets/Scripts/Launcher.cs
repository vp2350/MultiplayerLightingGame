using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;

    // Start is called before the first frame update
    public void OnClick_ConnectBtn()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectedScreen.SetActive(false);
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        disconnectedScreen.SetActive(false);
        connectedScreen.SetActive(true);

    }
}
