using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int playerNumber = 1;
    public GameManager gameManager;

    public GameObject fireWorksPrefab; // reference to our firework prefab;
    public Transform leftFireWorksPosition; // an empty transform to the left of our goal.
    public Transform rightFireWorksPosition; // an empty transform to the right of our goal.


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SoccerBall")
        {
            // score a goal
            Debug.Log("goal Score!");
            gameManager.IncreasePlayerScore(playerNumber);

            // spawn in our fireworks at the left and right positions respectively, and parent them to our AR parent.
            GameObject clone = Instantiate(fireWorksPrefab, leftFireWorksPosition.position, fireWorksPrefab.transform.rotation, gameManager.aRContentParent);
            Destroy(clone,5);
            clone = Instantiate(fireWorksPrefab, rightFireWorksPosition.position, fireWorksPrefab.transform.rotation, gameManager.aRContentParent);
            Destroy(clone, 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = playerNumber == 1 ? Color.red : Color.blue; // a short hand if statement
        Gizmos.DrawCube(transform.position, transform.localScale); // show our cube.
    }
}
