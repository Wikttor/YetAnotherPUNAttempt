using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FirstRPCCarrier : MonoBehaviour
{
    public static FirstRPCCarrier instance;
    private static PhotonView myPhotonView;
    public static Ping ping;

    public static void ButtonPressed()
    {
        if (instance == null)//now you should check if there is enough (more than one?) player in the room
        {
            Messages.NoRPCCarrier();
            PhotonNetwork.Instantiate("FirstPUNRPCCarrier", new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Messages.TimeStampedDisplayMessage("RPC sent to all clients");
            myPhotonView.RPC("RPC_MessageDisplayedOnAllClients", RpcTarget.AllBuffered, null);
        }
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            Messages.DisplayMessage("RPC Carrier Spawned successfully at " + Time.time.ToString()    );
            instance = this;
            myPhotonView = this.GetComponent<PhotonView>();
            if(myPhotonView == null)
            {
                Debug.Log("Could not get Photon View Component");
            }
        }
        else
        {
            Messages.DisplayMessage("Unneccesarily spawned surplus RPC Carrier at " + Time.time.ToString()   );
            Destroy(this);
        }
        this.gameObject.AddComponent<Ping>();
    }
    [PunRPC]
    void RPC_MessageDisplayedOnAllClients()
    {
        Messages.TimeStampedDisplayMessage("RPC addressed to all clients received");
    }
}
