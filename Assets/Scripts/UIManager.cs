using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playerOneCanvas; // reference to the player's canvas object
    public Text playerOneScoreText; // reference to the actual we'll be modifying.
    public Color playerOneColour; // the colour of the text we are going to be using.

    public GameObject playerTwoCanvas; // reference to the player's canvas object
    public Text playerTwoScoreText; // reference to the actual we'll be modifying.
    public Color playerTwoColour; // the colour of the text we are going to be using.

    
    /// <summary>
    /// Hide the canvas when we first start the game, until the ball has been dropped.
    /// </summary>
    /// <param name="displayScores"></param>
    public void DisplayScores(bool displayScores)
    {
        if(playerOneCanvas == null || playerTwoCanvas == null)
        {
            Debug.LogError("No Canavas has been assigned for this player");
            return; 
        }
        playerOneCanvas.SetActive(displayScores);
        playerTwoCanvas.SetActive(displayScores);
    }

    public void UpdateScores(int playerOneScore, int playerTwoScore)
    {
        
        if(playerOneScoreText == null || playerTwoScoreText == null)
        {
            Debug.LogError("No Text has been assigned for this player");
            return;
        }

        playerOneScoreText.color = playerOneColour; // change the colour of our text to match the player colour
        playerOneScoreText.text = playerOneScore.ToString(); // set the text to display our score.

        playerTwoScoreText.color = playerTwoColour; // change the colour of our text to match the player colour
        playerTwoScoreText.text = playerTwoScore.ToString(); // set the text to display our score.

    }

}
