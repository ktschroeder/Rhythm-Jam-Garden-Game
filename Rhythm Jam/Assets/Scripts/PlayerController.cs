using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f; // in seconds
    private Vector3 gridPos = new Vector3(1,1,0); // game-starting grid position. TODO this is magic and not directly tied to the player if they are dragged in the editor

    public TileBase tileA;
    public TileBase tileB;
    public Vector2Int size;
    public Tilemap tilemap;
    public TileController tileController;

    void Update(){
        if(Input.GetKey(KeyCode.W) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.up));
        if(Input.GetKey(KeyCode.A) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.left));
        if(Input.GetKey(KeyCode.S) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.down));
        if(Input.GetKey(KeyCode.D) && !isMoving)
            StartCoroutine(MovePlayer(Vector3.right));
            
        if(Input.GetKeyDown(KeyCode.E)){
            int playerNearGridX = (int)Math.Round(transform.position.x - 0.5f); // the -0.5f is because the sprite position is determined by its center, whereas the tilemap tiles are determined by their lower left corner
            int playerNearGridY = (int)Math.Round(transform.position.y - 0.5f); // this rounding lets a player perform an action on a grid spot while moving
            tileController.TestSwap(new Vector3Int(playerNearGridX, playerNearGridY, 0));
        }

    }

    private IEnumerator MovePlayer(Vector3 direction){
        isMoving = true;

        float elapsedTime = 0;
        origPos = transform.position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove){
            transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // this prevents any accumulation of imperfect movements

        isMoving = false;
    }
}
