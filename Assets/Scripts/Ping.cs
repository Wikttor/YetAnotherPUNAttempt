using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ping : MonoBehaviour
{
    private PhotonView myPhotonView;
    private const float pingFrequency = 4f;
    void Start()
    {
        Debug.Log("Component Ping added");
        myPhotonView = this.GetComponent<PhotonView>();
        StartCoroutine("Pinging");
    }

    IEnumerator Pinging()
    {
        object[] timeStamp = new object[1];

        Debug.Log("PingCoroutineStarted");
        while (true)
        {
            yield return new WaitForSeconds(pingFrequency);
            timeStamp[0] = Time.time;
            myPhotonView.RPC("RPC_Pong", RpcTarget.Others, timeStamp);
        }
    }

    [PunRPC]
    void RPC_Pong(float time)
    {
        object[] timeStamp = new object[1];
        timeStamp[0] = time;
        myPhotonView.RPC("RPC_CalculatePing", RpcTarget.Others, timeStamp);
    }
    [PunRPC]
    void RPC_CalculatePing(float time)
    {
        Messages.DisplayMessage("Ping is " + (Time.time - time).ToString());
    }
}
