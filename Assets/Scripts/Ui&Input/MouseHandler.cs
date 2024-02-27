using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public LayerMask layersToHit;// the layers we are going to allowed to hit.
    public GameManager gameManager; // a reference to our game manager;



    // Update is called once per frame
    void Update()
    {
        GetMouseInput();
    }

    /// <summary>
    /// Gets input from the player mouse/tap on the screen.
    /// </summary>
    void GetMouseInput()
    {
        if(Input.GetMouseButtonDown(0))// primary mouse input/touch input.
        {
            RaycastHit hit; // data stored based on what we've hit.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // draws a ray from my camera to my mouse position in the world.

            // Do our ray cast, if we hit something or blocks the ray, store the data in hit.
            if (Physics.Raycast(ray, out hit, layersToHit))
            {
                gameManager.SpawnOrMoveSoccerBall(hit.point);// the point in the world where the ray has hit, spawn our soccerball, or move it!
            }
        }
    }
}
