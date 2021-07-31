using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f; // in seconds
    private Vector3 gridPos = new Vector3(1,1,0); // game-starting grid position. This is magic and not directly tied to the player if they are dragged in the editor
    private bool rightFootNext = true;
    private InstrumentEnum.Instrument instrumentSelection = InstrumentEnum.Instrument.Piano;

    [FMODUnity.EventRef]
	public string FootstepsEvent = "";

    public TileController tileController;

    public Sprite[] spriteArray; // First 4 are downward. Next 4 are upward. Next 2 are leftward. Last 2 are rightward.
    public SpriteRenderer spriteRenderer;


    void Start(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(Input.GetKey(KeyCode.W) && !isMoving && transform.position.y <= tileController.gridHeight - 0)
            StartCoroutine(MovePlayer(Vector3.up));
        if(Input.GetKey(KeyCode.A) && !isMoving && transform.position.x >= 1)
            StartCoroutine(MovePlayer(Vector3.left));
        if(Input.GetKey(KeyCode.S) && !isMoving && transform.position.y >= 2)
            StartCoroutine(MovePlayer(Vector3.down));
        if(Input.GetKey(KeyCode.D) && !isMoving && transform.position.x <= tileController.gridLength - 1)
            StartCoroutine(MovePlayer(Vector3.right));

        if(Input.GetKey(KeyCode.Alpha1))
            instrumentSelection = InstrumentEnum.Instrument.Piano;
        if(Input.GetKey(KeyCode.Alpha2))
            instrumentSelection = InstrumentEnum.Instrument.Organ;
        if(Input.GetKey(KeyCode.Alpha3))
            instrumentSelection = InstrumentEnum.Instrument.Guitar;
        if(Input.GetKey(KeyCode.Alpha4))
            instrumentSelection = InstrumentEnum.Instrument.Drums;
        if(Input.GetKey(KeyCode.Alpha5))
            instrumentSelection = InstrumentEnum.Instrument.Bass;
            
        if(Input.GetKeyDown(KeyCode.E)){
            int playerNearGridX = (int)Math.Round(transform.position.x - 0.5f); // the -0.5f is because the sprite position is determined by its center, whereas the tilemap tiles are determined by their lower left corner
            int playerNearGridY = (int)Math.Round(transform.position.y - 1.28f); // this rounding lets a player perform an action on a grid spot while moving

            //plant a plant
            tileController.Plant(new Vector3Int(playerNearGridX, playerNearGridY, 0), instrumentSelection);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            int playerNearGridX = (int)Math.Round(transform.position.x - 0.5f); // the -0.5f is because the sprite position is determined by its center, whereas the tilemap tiles are determined by their lower left corner
            int playerNearGridY = (int)Math.Round(transform.position.y - 1.28f); // this rounding lets a player perform an action on a grid spot while moving

            //uproot a plant
            tileController.Uproot(new Vector3Int(playerNearGridX, playerNearGridY, 0));
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            int playerNearGridX = (int)Math.Round(transform.position.x - 0.5f); // the -0.5f is because the sprite position is determined by its center, whereas the tilemap tiles are determined by their lower left corner
            int playerNearGridY = (int)Math.Round(transform.position.y - 1.28f); // this rounding lets a player perform an action on a grid spot while moving

            //plant a plant
            tileController.Water(new Vector3Int(playerNearGridX, playerNearGridY, 0));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

    private IEnumerator MovePlayer(Vector3 direction){
        isMoving = true;

        PlayFootstepSound(direction);

        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove){
            if (direction == Vector3.down) { // animation
                if(elapsedTime > 0.25f * timeToMove && elapsedTime < 0.75f * timeToMove){
                    if(rightFootNext)
                        spriteRenderer.sprite = spriteArray[1];
                    else
                        spriteRenderer.sprite = spriteArray[3];
                    
                }
                else
                    spriteRenderer.sprite = spriteArray[0];
            }
            if (direction == Vector3.up) { // animation
                if(elapsedTime > 0.25f * timeToMove && elapsedTime < 0.75f * timeToMove){
                    if(rightFootNext)
                        spriteRenderer.sprite = spriteArray[7];
                    else
                        spriteRenderer.sprite = spriteArray[5];
                    
                }
                else
                    spriteRenderer.sprite = spriteArray[4];
            }
            if (direction == Vector3.left) { // animation
                if(elapsedTime > 0.25f * timeToMove && elapsedTime < 0.75f * timeToMove)
                        spriteRenderer.sprite = spriteArray[9];
                else
                    spriteRenderer.sprite = spriteArray[8];
            }
            if (direction == Vector3.right) { // animation
                if(elapsedTime > 0.25f * timeToMove && elapsedTime < 0.75f * timeToMove)
                        spriteRenderer.sprite = spriteArray[11];
                else
                    spriteRenderer.sprite = spriteArray[10];
            }

            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // this prevents any accumulation of imperfect movements

        isMoving = false;
        rightFootNext = !rightFootNext;
    }

    private void PlayFootstepSound(Vector3 direction)
	{
		if(FootstepsEvent != null)
		{
			FMOD.Studio.EventInstance movement = FMODUnity.RuntimeManager.CreateInstance(FootstepsEvent);
			movement.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

            if (direction == Vector3.up) {
                movement.setParameterByName("up", 1.0f);
            } else if (direction == Vector3.down) {
                movement.setParameterByName("down", 1.0f);
            } else if (direction == Vector3.left) {
                movement.setParameterByName("left", 1.0f);
            } else {
                movement.setParameterByName("right", 1.0f);
            }
			
			movement.start();
			movement.release();//Release each event instance immediately, there are fire and forget, one-shot instances. 
		}
	}
}
