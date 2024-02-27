using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{
    public AudioClip fireWorkSound; // the firework sound
    public AudioSource audioSource; // reference to our audiosource.
    public int numberOfFireworks = 3; // the number of fireworks that will be spawned
    public float initialDelay = 2; // an initial delay before the first firework is spawned.
    public float timeBetweenFireWorks = 0.5f;// half a second between each firework


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayFireworks()); // start our coroutine up here.
    }

    /// <summary>
    /// a coroutine that allows us to dictate when certain of code should be played, this allows us to delay certain parts of code.
    /// but it also allows us do more complex actions.
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayFireworks()
    {
        yield return new WaitForSeconds(initialDelay); // wait a couple of seconds before continuing with our code.
        for(int i =0; i<numberOfFireworks; i++)
        {
            audioSource.PlayOneShot(fireWorkSound); // play our fire works sound once.
            yield return new WaitForSeconds(timeBetweenFireWorks); // now wait before before we iterate to the next part of the for loop.
        }

        yield return null;
    }
}
