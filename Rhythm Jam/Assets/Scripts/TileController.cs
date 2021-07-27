using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    private Tilemap tilemap;
    
    public TileBase grass;
    public TileBase plantspot;
    public int gridHeight = 9;
    public int gridLength = 13;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
    }

    // returns the name of the tile's sprite at this position, e.g. "grass"
    public string GetTileName(Vector3Int pos){
        if(tilemap == null){
            print("tilemap not found");
            return null;
        } 
        if(pos.x < 0 || pos.x > gridLength - 1 || pos.y < 0 || pos.y > gridHeight - 1){
            print($"invalid grid position: {pos}");
            return null;
        }
        
        return tilemap.GetTile<Tile>(pos).name;
    }

    // toggles a spot. This is only meant for testing purposes
    public void TestSwap(Vector3Int pos){
        var name = GetTileName(pos);
        if(name == null)
            return;
        if(name == "grass"){
            tilemap.SetTile(pos, plantspot);
        }
        else tilemap.SetTile(pos, grass);
    }
}
