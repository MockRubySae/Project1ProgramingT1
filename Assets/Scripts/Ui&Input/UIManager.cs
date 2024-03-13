using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // reference to the player's canvas object
    public GameObject playerOneCanvas;
    // reference to the actual we'll be modifying.
    public Text playerOneScoreText;
    // the colour of the text we are going to be using.
    public Color playerOneColour;

    // reference to the player's canvas object
    public GameObject playerTwoCanvas;
    // reference to the actual we'll be modifying.
    public Text playerTwoScoreText;
    // the colour of the text we are going to be using.
    public Color playerTwoColour;

    
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
        // change the colour of our text to match the player colour
        playerOneScoreText.color = playerOneColour;
        // set the text to display our score.
        playerOneScoreText.text = playerOneScore.ToString();

        // change the colour of our text to match the player colour
        playerTwoScoreText.color = playerTwoColour;
        // set the text to display our score.
        playerTwoScoreText.text = playerTwoScore.ToString();

    }

}
