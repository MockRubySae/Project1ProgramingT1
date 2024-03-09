﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{

    /// <summary>
    /// The different states that our character can be in
    /// </summary>
    public enum CharacterStates {Idle,Roaming,Waving,Playing,Fleeing}

    // the current state that our character is in.
    public CharacterStates currentCharacterState;




    // a reference to our game manager
    public GameManager gameManager;
    // reference to our rigidbody
    public Rigidbody rigidBody;

    // roaming state variabless
    // the target we are currently heading towards
    private Vector3 currentTargetPosition;
    // the last target we were heading towards.
    private Vector3 previousTargetPosition;
    // how fast our character is moving.
    public float moveSpeed = 3;
    // how close should we get to our target?
    public float minDistanceToTarget = 1;

    // idle state variables
    // Once we reach our target position how long should we wait till we get another position?
    public float idleTime = 2;
    // the time we are waiting till, we can move again.
    private float currentIdleWaitTime;

    // Waving state varaiables
    // the time spent waving
    public float waveTime = 2;
    // the current time to wave till.
    private float currentWaveTime;
    // the distance that will be checking to see if we are in range to wave at another character.
    public float distanceToStartWavingFrom = 4f;
    // a collection of references to all characters in our scene.
    private CharacterController[] allCharactersInScene;
    // the time between when we are allowed to wave again.
    public float timeBetweenWaves = 5;
    // the current time for our next wave to be iniated.
    private float currentTimeBetweenWaves;

    // Fleeing state variables
    // the distance that is "to" close for the player to be to us.
    public float distanceThresholdOfPlayer = 5;


    // Playing state variables
    // a reference to the current soccerball;
    private Transform currentSoccerBall = null;
    // a reference to our identification colour.
    public GameObject selfIdentifier;
    // reference to this characters goal.
    public GameObject myGoal; 
    // the amount of force the character can use to kick the ball.
    public float soccerBallKickForce = 10;
    // if the soccerball is close nough, then we can kick it.
    public float soccerBallInteractDistance = 0.25f;
    // a delay of the soccer animation before they kick.
    public float passingAnimationDelay = 0.5f;
    // the time at which the animation will play and we should kick
    private float currentTimeTillPassingAnimationPlays;
    // a reference to our animation handler script.
    public AnimationHandler animationHandler; 

    /// <summary>
    /// Returns the currentTargetPosition
    /// And Sets the new current position
    /// </summary>
    private Vector3 CurrentTargetPosition
    {
        get
        {
            // gets the current value
            return currentTargetPosition; 
        }
        set
        {
            // assign our current position to our previous target position
            previousTargetPosition = currentTargetPosition;
            // assign the new value to our current target position
            currentTargetPosition = value; 
        }
    }

    // called each time the script or the game object is disabled
    private void OnDisable()
    {
        // if the game is not null
        if (gameManager != null) 
        {
            // then tell it there are no characters in range.
            gameManager.RunningAwayFromPlayer(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get a random starting position
        CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
        // find the references to all characters in our scene.
        allCharactersInScene = FindObjectsOfType<CharacterController>();
        // set the character by default to start roaming
        currentCharacterState = CharacterStates.Roaming;

        selfIdentifier.SetActive(false);
        // set our animation to idle
        animationHandler.CurrentState = AnimationHandler.AnimationState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        // always look towards the position we are aiming for.
        LookAtTargetPosition();

        // call our state manager
        StateManager();
    }

    private void StateManager()
    {
        // check what state we are in
        if (currentCharacterState == CharacterStates.Roaming)
        {
            // call our roaming state function
            HandleRoamingState();
        }
        else if (currentCharacterState == CharacterStates.Playing)
        {
            // call our playing state function
            HandlePlayingState();
        }
        else if (currentCharacterState == CharacterStates.Idle)
        {
            // call our idle state function
            HandleIdleState();
        }
        else if (currentCharacterState == CharacterStates.Waving)
        {
            // call our waving state function
            HandleWavingState();
        }
        else if (currentCharacterState == CharacterStates.Fleeing)
        {
            // call our fleeing state function
            HandleFleeingState();
        }
    }



    /// <summary>
    /// Handles the Roaming state of our character
    /// </summary>
    private void HandleRoamingState()
    {
        float distanceToTarget = 0;

        if (currentSoccerBall != null)
        {
            distanceToTarget = soccerBallInteractDistance;
        }
        else
        {
            distanceToTarget = minDistanceToTarget;
        }

        /// if we are still too far away move closer
        if (Vector3.Distance(transform.position, CurrentTargetPosition) > distanceToTarget)
        {
            if (currentSoccerBall != null)
            {
                // here would should be running
                if (animationHandler.CurrentState != AnimationHandler.AnimationState.Running)
                {
                    animationHandler.CurrentState = AnimationHandler.AnimationState.Running; // set our animation to running animation
                }

                CurrentTargetPosition = currentSoccerBall.position;
                Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); // the positon we want to move towards
                Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime * 1.5f); // the amount we should move towards that position
                rigidBody.MovePosition(nextMovePosition);
                currentIdleWaitTime = Time.time + idleTime;
            }
            else
            {
                // here would should be running
                if (animationHandler.CurrentState != AnimationHandler.AnimationState.Walking)
                {
                    animationHandler.CurrentState = AnimationHandler.AnimationState.Walking; // set our animation to walking animation
                }
                Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); // the positon we want to move towards
                Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); // the amount we should move towards that position
                rigidBody.MovePosition(nextMovePosition);
                currentIdleWaitTime = Time.time + idleTime;
            }
        }
        else if (currentSoccerBall != null)
        {
            // start playing with the ball
            currentCharacterState = CharacterStates.Playing;
            // sets the time to wait till until we play the animation
            currentTimeTillPassingAnimationPlays = Time.time + passingAnimationDelay;
        }
        else
        {
            currentCharacterState = CharacterStates.Idle; // start idling

        }
    }
    /// <summary>
    /// Handle the idle state of our character
    /// </summary>
    private void HandleIdleState()
    {
        if(currentCharacterState == CharacterStates.Idle)
        {
            // we must be close enough to our target position.
            // we wait a couple seconds.
            // then find a new position to move to.
            if (Time.time > currentIdleWaitTime)
            {
                // lets find a new position
                CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
                currentCharacterState = CharacterStates.Roaming; // start roaming again
            }
            // here would should be running
            if (animationHandler.CurrentState != AnimationHandler.AnimationState.Idle)
            {
                animationHandler.CurrentState = AnimationHandler.AnimationState.Idle; // set our animation to idle animation
            }
        }
    }

    /// <summary>
    /// Handles the fleeing state of our character
    /// </summary>
    private void HandleFleeingState()
    {
        if (currentCharacterState != CharacterStates.Fleeing && gameManager.IsPlayerToCloseToCharacter(transform, distanceThresholdOfPlayer))
        {
            // we should be fleeing
            currentCharacterState = CharacterStates.Fleeing;
            gameManager.RunningAwayFromPlayer(true); // we are fleeing from the player play our music.
            // here would should be running
            if (animationHandler.CurrentState != AnimationHandler.AnimationState.Running)
            {
                animationHandler.CurrentState = AnimationHandler.AnimationState.Running; // set our animation to running animation
            }

        }
        else if (currentCharacterState == CharacterStates.Fleeing && gameManager.IsPlayerToCloseToCharacter(transform, distanceThresholdOfPlayer))
        {
            /// if we are still too far away move closer
            if (currentCharacterState == CharacterStates.Fleeing && Vector3.Distance(transform.position, CurrentTargetPosition) > minDistanceToTarget)
            {
                Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); // the positon we want to move towards
                Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime * 1.5f); // the amount we should move towards that position
                rigidBody.MovePosition(nextMovePosition);
            }
            else
            {
                CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
            }

        }
        else if (currentCharacterState == CharacterStates.Fleeing && gameManager.IsPlayerToCloseToCharacter(transform, distanceThresholdOfPlayer) == false)
        {
            // if we are still fleeing, then we want to transition back to our roaming state.
            currentCharacterState = CharacterStates.Roaming;
            CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
            gameManager.RunningAwayFromPlayer(false); // stop fleeing from the player so go back to the original music.
            // here would should be walking
          
        }
    }

    /// <summary>
    /// Handles the playing state of our character
    /// </summary>
    private void HandlePlayingState()
    {
        // we want to kick the ball cause are close enough.
        if(currentCharacterState == CharacterStates.Playing)
        {
            // here would should be running
            if (animationHandler.CurrentState != AnimationHandler.AnimationState.Passing)
            {
                animationHandler.CurrentState = AnimationHandler.AnimationState.Passing; // set our animation to running animation
            }

            if (Time.time > currentTimeTillPassingAnimationPlays)
            {
                KickSoccerBall(); // kick our ball
                                  // set our target to the soccer ball again, and start moving towards the ball again.
                CurrentTargetPosition = currentSoccerBall.position;
                currentCharacterState = CharacterStates.Roaming;
            }        
        }
    }

    /// <summary>
    /// Handles the waving state.
    /// </summary>
    private void HandleWavingState()
    {
        if (ReturnCharacterTransformToWaveAt() != null && currentCharacterState != CharacterStates.Waving && Time.time > currentTimeBetweenWaves && currentCharacterState != CharacterStates.Fleeing && currentSoccerBall == null)
        {
            // we should start waving!
            currentCharacterState = CharacterStates.Waving;
            currentWaveTime = Time.time + waveTime; // set up the time we should be waving till.
            CurrentTargetPosition = ReturnCharacterTransformToWaveAt().position; // set the current target position  to the closest transform, so that way we also rotate towards it.
            // here would should be Waving
            if (animationHandler.CurrentState != AnimationHandler.AnimationState.Waving)
            {
                animationHandler.CurrentState = AnimationHandler.AnimationState.Waving; // set our animation to waving animation
            }
        }
        if (currentCharacterState == CharacterStates.Waving && Time.time > currentWaveTime)
        {
            // Stop waving
            CurrentTargetPosition = previousTargetPosition; // resume moving towards our random target position.
            currentTimeBetweenWaves = Time.time + timeBetweenWaves; // set the next time for when we can wave again.
            currentCharacterState = CharacterStates.Roaming; // start roaming again
        }

    }

    /// <summary>
    /// Returns a transform if they are in range of the player to be waved at.
    /// </summary>
    /// <returns></returns>
    private Transform ReturnCharacterTransformToWaveAt()
    {
        // looping through all the characters in our scene
        for(int i = 0; i<allCharactersInScene.Length; i++)
        {
            // if the current element we are up to is not equal to this instance our character.
            if (allCharactersInScene[i] != this)
            {
                //check the distance between them, if they are close enough return that other character
                if(Vector3.Distance(transform.position, allCharactersInScene[i].transform.position) < distanceToStartWavingFrom)
                {                 
                    // but also let's return the character we should be waving at.
                    return allCharactersInScene[i].transform;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Rotates our character to always face the direction we are heading.
    /// </summary>
    private void LookAtTargetPosition()
    {
        Vector3 directionToLookAt = CurrentTargetPosition - transform.position; // get the direction we should be lookin at
        directionToLookAt.y = 0;//don't change the Y position
        Quaternion rotationOfDirection = Quaternion.LookRotation(directionToLookAt); // get a rotation that we can use to look towards
        transform.rotation = rotationOfDirection; // set our current rotation to our rotation to face towards.
    }

    private void OnDrawGizmosSelected()
    {
        // draw a blue sphere on the position we are moving towards.
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(CurrentTargetPosition, 0.5f);
    }

    /// <summary>
    /// Is called when the soccer ball is spawned in
    /// </summary>
    /// <param name="SoccerBall"></param>
    public void SoccerBallSpawned(Transform SoccerBall)
    {
        currentSoccerBall = SoccerBall; // assign the soccer ball to our reference
        CurrentTargetPosition = currentSoccerBall.position; // set our target position to our soccer ball
        currentCharacterState = CharacterStates.Roaming; // using our roaming state to start moving towards our soccerball
        selfIdentifier.SetActive(true);
    }

    /// <summary>
    /// Handles Kicking the soccerball
    /// </summary>
    public void KickSoccerBall()
    {
        Vector3 direction = myGoal.transform.position - transform.forward; // get a directional vector that moves towards our goal post.
        currentSoccerBall.GetComponent<Rigidbody>().AddForce(direction * soccerBallKickForce * Random.Range(0.5f, 10f)); // kick the ball towards our goal post. and add a little random force so the ball doesn't get stuck
    }

}
