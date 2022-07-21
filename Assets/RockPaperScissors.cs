using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissors : MonoBehaviour
{
    public enum RPS
    {
        rock,
        paper,
        scissors
    }
    public RPS thisButton;

    public void ButtonClicked()
    {
        TurnSync.PlayRockPaperScissors(thisButton);
    }
}
