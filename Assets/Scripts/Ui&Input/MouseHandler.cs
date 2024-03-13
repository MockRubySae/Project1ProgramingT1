using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    // the layers we are going to allowed to hit.
    public LayerMask layersToHit;
    // a reference to our game manager;
    public GameManager gameManager;



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
        // primary mouse input/touch input.
        if (Input.GetMouseButtonDown(0))
        {
            // data stored based on what we've hit.
            RaycastHit hit;
            // draws a ray from my camera to my mouse position in the world.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Do our ray cast, if we hit something or blocks the ray, store the data in hit.
            if (Physics.Raycast(ray, out hit, layersToHit))
            {
                // the point in the world where the ray has hit, spawn our soccerball, or move it!
                gameManager.SpawnOrMoveSoccerBall(hit.point);
            }
        }
    }
}
