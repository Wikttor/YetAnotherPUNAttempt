using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyPUNBacis : MonoBehaviourPunCallbacks
{
    private bool joinedRoom = false;
    public Messages myMessages;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (joinedRoom)
        {
            Debug.Log("We joined the room");
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        //TextViaCoroutine.singleton.proTextMesh.text = "Successfully joined the lobby";
        myMessages.PUNConnectedSuccessfully();
        PhotonNetwork.CreateRoom("dupa");
        StartCoroutine(JoinRoom());
        Debug.Log("not crashed");

    }
    public override void OnJoinedRoom()
    {
        joinedRoom = true;
        Debug.Log("joined ther rooom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    public IEnumerator JoinRoom()
    {
        yield return new WaitForSeconds(3f);
        if (!joinedRoom)
        {
            PhotonNetwork.JoinRoom("dupa");
        }
    }
}
