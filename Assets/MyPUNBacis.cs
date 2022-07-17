using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyPUNBacis : MonoBehaviourPunCallbacks
{
    public Messages myMessages;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        //TextViaCoroutine.singleton.proTextMesh.text = "Successfully joined the lobby";
        myMessages.PUNConnectedSuccessfully();
    }
}
