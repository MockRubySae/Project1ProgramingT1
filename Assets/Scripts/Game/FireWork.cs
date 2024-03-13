using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{
    // the firework sound
    public AudioClip fireWorkSound;
    // reference to our audiosource.
    public AudioSource audioSource;
    // the number of fireworks that will be spawned
    public int numberOfFireworks = 3;
    // an initial delay before the first firework is spawned.
    public float initialDelay = 2;
    // half a second between each firework
    public float timeBetweenFireWorks = 0.5f;


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
        // wait a couple of seconds before continuing with our code.
        yield return new WaitForSeconds(initialDelay);
        for(int i =0; i<numberOfFireworks; i++)
        {
            // play our fire works sound once.
            audioSource.PlayOneShot(fireWorkSound);
            // now wait before before we iterate to the next part of the for loop.
            yield return new WaitForSeconds(timeBetweenFireWorks);
        }

        yield return null;
    }
}
