using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    public AudioClip roamingMusic; // the music we will use when roaming.
    public AudioClip playingMusic; // the music we will use whilst they play soccer.
    public AudioClip fleeingMusic; // play a people screaming sound.

    private AudioClip currentTrack; // the current track being played.
    private AudioClip previousTrack; // the previous track that was played.
    public AudioSource audioSource; // a reference to our audiosource, where the music will be played from.

    /// <summary>
    /// So this gets called everytime the script get's turn off/on
    /// </summary>
    private void OnEnable()
    {
        if(currentTrack == null)
        {
            currentTrack = roamingMusic; // just music to default
        }
        ChangeTrack(currentTrack); // start playing our music
    }

    /// <summary>
    /// Plays the roaming music that is played at the start of the game whilst roaming.
    /// </summary>
    public void PlayRoamingMusic()
    {
        currentTrack = roamingMusic;
        ChangeTrack(currentTrack);
    }

    /// <summary>
    /// Plays playing music that is used during the soccer
    /// </summary>
    public void PlayPlayingMusic()
    {
        currentTrack = playingMusic;
        ChangeTrack(currentTrack);
    }

    /// <summary>
    /// Plays the fleeing music
    /// </summary>
    public void PlayFleeingMusic()
    {
        currentTrack = fleeingMusic;
        ChangeTrack(currentTrack);
    }

    /// <summary>
    /// Play the previous track that was being played
    /// </summary>
    public void PlayPreviousTrack()
    {
        // if there is no previous track
        if(previousTrack == null)
        {
            return; 
        }
        currentTrack = previousTrack; // set the current track to the previous track
        ChangeTrack(currentTrack); // play our previous track
    }

    /// <summary>
    /// This function changes the clip being played at the momenet
    /// </summary>
    /// <param name="clip"></param>
    private void ChangeTrack(AudioClip clip)
    {
        audioSource.Stop(); // stop playing the current clip.
        if(audioSource.clip != clip) // if the current clip in the audio source is not equal to the clip we are trying to play
        {
            previousTrack = audioSource.clip; // store the previous track
            audioSource.clip = clip; // set the new track
        }
        audioSource.loop = true; // set the track to be looping.
        audioSource.Play(); // start playing our music
    }
}
