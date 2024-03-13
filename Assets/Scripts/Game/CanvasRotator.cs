using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // get the relative direction of the canvas to the players camera.
        Vector3 relativePosition = transform.position - Camera.main.transform.position;
        // 0 our the y axis as we don't want it to rotate on that axis.
        relativePosition.y = 0;
        // create a rotation to look at the direction we are wanting.
        Quaternion rotation = Quaternion.LookRotation(relativePosition);
        // set our new rotation to our current rotation.
        transform.rotation = rotation;
    }
}
