using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    private Tilemap tilemap;
    public TileBase grass;
    public TileBase plantspot; 

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
    }

    // returns the name of the tile's sprite at this position, e.g. "grass"
    public string GetTileName(Vector3Int pos){
        if(tilemap == null)
            return null;

        // TODO check position is valid
        
        return tilemap.GetTile<Tile>(pos).name;
    }

    // toggles a spot. This is only meant for testing purposes
    public void TestSwap(Vector3Int pos){
        var name = tilemap.GetTile<Tile>(pos).name;
        if(name == "grass"){
            tilemap.SetTile(pos, plantspot);
        }
        else tilemap.SetTile(pos, grass);
    }
}
