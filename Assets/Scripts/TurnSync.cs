using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurnSync : MonoBehaviour
{
    private RockPaperScissors.RPS myChoice;
    public static TurnSync instance;
    private PhotonView myPhotonView;
    public int turnID = 0;
    public enum turnState
    {
        NotStarted,
        Active,
        Waiting,
        Auto
    }
    public static turnState state = turnState.NotStarted;

    private void OnEnable()
    {
        myPhotonView =  this.gameObject.GetComponent<PhotonView>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void ButtonPressed()
    {
        switch (state)
        {
            case turnState.NotStarted:
                instance.StartGame();
                break;
            case turnState.Active:
                instance.DeclareEndOfTurn();
                break;
            case turnState.Waiting:
                instance.CancelEOT();
                break;
            case turnState.Auto:

                break;        
        }
    }
    [PunRPC]
    public void RPC_StartGame()
    {
        ChangeAndCheckTurnstate(turnState.NotStarted, turnState.Active);
        turnID++;
        Messages.DisplayMessage("Started turn number " + (turnID).ToString()    );      
    }
    public void StartGame()
    {
        if (state == turnState.NotStarted)
        {
            myPhotonView.RPC("RPC_StartGame", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    public void RPC_EndOfTurn(int endingTurnID)
    {
        if(turnID == endingTurnID)
        {
            state = turnState.Auto;
            Messages.DisplayMessage("Finished turn number " + turnID.ToString());
            StartCoroutine(StartNewTurnAfterDelay() );
        }
        else
        {
            Debug.Log("TurnID does not match while ending the turn");
        }       
    }


    IEnumerator StartNewTurnAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        turnID++;
        ChangeAndCheckTurnstate(turnState.Auto, turnState.Active);
        Messages.DisplayMessage("Started turn number " + (turnID).ToString());
    }

    private void ChangeAndCheckTurnstate( turnState oldState, turnState newState)
    {
        if (state == oldState)
        {
            state = newState;
        }
        else
        {
            Messages.DisplayMessage("Something went south with the turn order");
        }
    }

    void DeclareEndOfTurn()
    {
        state = turnState.Waiting;
        myPhotonView.RPC("RPC_OpponentDeclaredEOT", RpcTarget.OthersBuffered, null);
    }

    public static void PlayRockPaperScissors(RockPaperScissors.RPS choice)
    {
        instance.myChoice = choice;
        object[] rpcParams = new object[1];
        rpcParams[0] = choice;
        state = turnState.Waiting;
        instance.myPhotonView.RPC("OpponentSentHisRPCChoice", RpcTarget.Others, rpcParams);
        //instance.myPhotonView.RPC("RPC_OpponentDeclaredEOT", RpcTarget.OthersBuffered, null); 

    }
    [PunRPC]
    void OpponentSentHisRPCChoice(RockPaperScissors.RPS choice)
    {
        if (state == turnState.Waiting)
        {
            object[] rpcParams = new object[1];
            rpcParams[0] = turnID;
            RPSResolve(choice);
            myPhotonView.RPC("RPC_EndOfTurn", RpcTarget.AllViaServer, rpcParams);
        }
        else
        {
            Messages.DisplayMessage("Opponent is waiting to end this turn");
        }
    }

    void RPSResolve(RockPaperScissors.RPS opponentsChoice)
    {
        if(
            (myChoice == RockPaperScissors.RPS.rock && opponentsChoice == RockPaperScissors.RPS.scissors) ||
            (myChoice == RockPaperScissors.RPS.paper && opponentsChoice == RockPaperScissors.RPS.rock) ||
            (myChoice == RockPaperScissors.RPS.scissors&& opponentsChoice == RockPaperScissors.RPS.paper)
            )
        {
            myPhotonView.RPC("RPC_RPSYouLost", RpcTarget.Others, null);
            Messages.DisplayMessage("You won");
        }
        else if (
            (myChoice == RockPaperScissors.RPS.rock && opponentsChoice == RockPaperScissors.RPS.paper) ||
            (myChoice == RockPaperScissors.RPS.paper && opponentsChoice == RockPaperScissors.RPS.scissors) ||
            (myChoice == RockPaperScissors.RPS.scissors && opponentsChoice == RockPaperScissors.RPS.rock)
            )
        {
            myPhotonView.RPC("RPC_RPSYouWon", RpcTarget.Others, null);
            Messages.DisplayMessage("You lost");
        }
        else
        {
            myPhotonView.RPC("RPC_RPSYouTied", RpcTarget.Others, null);
            Messages.DisplayMessage("You Tied");
        }
    }
    [PunRPC]
    void RPC_RPSYouWon()
    {
        Messages.DisplayMessage("You won");
    }
    [PunRPC]
    void RPC_RPSYouLost()
    {
        Messages.DisplayMessage("You lost");
    }
    [PunRPC]
    void RPC_RPSYouTied()
    {
        Messages.DisplayMessage("You Tied");
    }
    [PunRPC]
    void RPC_OpponentDeclaredEOT()
    {
        if(state == turnState.Waiting)
        {
            object[] rpcParams = new object[1];
            rpcParams[0] = turnID;
            myPhotonView.RPC("RPC_EndOfTurn", RpcTarget.AllViaServer,rpcParams );
        }
        else
        {
            Messages.DisplayMessage("Opponent is waiting to end this turn");
        }
    }

    void CancelEOT()
    {
        myPhotonView.RPC("RPC_OpponentCancelledEOT", RpcTarget.OthersBuffered, null);
    }

    [PunRPC]
    void RPC_OpponentCancelledEOT()
    {
        if (state == turnState.Active)
        {
            Messages.DisplayMessage("Opponent is no longer waiting for the end of turn");
            myPhotonView.RPC("RPC_EOTCancellationConfirmed", RpcTarget.OthersBuffered, null);
        }
    }
    [PunRPC]
    void RPC_EOTCancellationConfirmed()
    {
        if(state == turnState.Waiting)
        {
            state = turnState.Active;
            Messages.DisplayMessage("End Of Turn cancellation succesfull");
        }
        else
        {
            Messages.DisplayMessage("End Of Turn cancellation failed for unknown reasons");
        }
    }
}
