//revised from https://gist.github.com/WickedJumper/bd44ed1c67080ecc3b98073b75a25bbd
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
	//FMOD Studio variables
	//The FMOD Studio Event path.
	//This script is designed for use with an event that has a game parameter for each of the surface variables, but it will still compile and run if they are not present.
	[FMODUnity.EventRef]
	public string FootstepsEvent = "";
    public PlayerController playerController;
    private float up;
	private float down;
	private float left;
	private float right;
	void Start()
	{
        up = 0.0f;
		down = 0.0f;
		left = 0.0f;
		right = 0.0f;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.W) && playerController.isMoving ){
            up = 1.0f;
            PlayFootstepSound();
        }
        if(Input.GetKeyDown(KeyCode.A) && playerController.isMoving ){
            left = 1.0f;
            PlayFootstepSound();
        }
        if(Input.GetKeyDown(KeyCode.S) && playerController.isMoving ){
            down = 1.0f;
            PlayFootstepSound();
        }
        if(Input.GetKeyDown(KeyCode.D) && playerController.isMoving){
            right =1.0f;
            PlayFootstepSound();
        }
        up = 0.0f;
		down = 0.0f;
		left = 0.0f;
		right = 0.0f;
	}

	void PlayFootstepSound()
	{
		if(FootstepsEvent != null)
		{
			FMOD.Studio.EventInstance movement = FMODUnity.RuntimeManager.CreateInstance(FootstepsEvent);
			movement.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

            Debug.Log("up: " + up + " down: " + down + " left: " + left + " right: " + right);
			movement.setParameterByName("up", up);
            movement.setParameterByName("down", down);
            movement.setParameterByName("left", left);
            movement.setParameterByName("right", right);

			movement.start();
			movement.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
		}
	}

}