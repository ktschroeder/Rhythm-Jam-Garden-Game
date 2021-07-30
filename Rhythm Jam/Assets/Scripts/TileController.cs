using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public TileBase grass;
    public TileBase plantspot;

    public TileBase bassNew;
    public TileBase bassGrown;
    public TileBase drumkitNew;
    public TileBase drumkitGrown;
    public TileBase guitarNew;
    public TileBase guitarGrown;
    public TileBase pianoNew;
    public TileBase pianoGrown;
    public TileBase pipeorganNew;
    public TileBase pipeorganGrown;

    public int gridHeight = 9;
    public int gridLength = 13;

    // Start is called before the first frame update
    void Start()
    {
        // tilemap = gameObject.GetComponent<Tilemap>();
    }

    // returns the name of the tile's sprite at this position, e.g. "grass"
    public string GetTileName(Vector3Int pos){
        if(tilemaps[pos.y] == null){
            print("tilemap not found");
            return null;
        } 
        if(pos.x < 0 || pos.x > gridLength - 1 || pos.y < 0 || pos.y > gridHeight - 1){
            print($"invalid grid position: {pos}");
            return null;
        }
        
        Tile tile = tilemaps[pos.y].GetTile<Tile>(pos);
        if(tile == null)
            return null;

        return tile.name;
    }

    // toggles a spot. This is only meant for testing purposes
    public void TestSwap(Vector3Int pos){
        var name = GetTileName(pos);
        if(name == null)
            return;
        if(name == "grass"){
            tilemaps[0].SetTile(pos, plantspot);
        }
        else tilemaps[0].SetTile(pos, grass);
    }

    // plants a type of plant (instrument) at position pos
    public void Plant(Vector3Int pos, InstrumentEnum.Instrument instrument){
        TileBase selectedPlant = drumkitGrown;
        bool duplicate = false;

        switch (instrument)
        {
            case InstrumentEnum.Instrument.Piano:
                selectedPlant = pianoNew;
                break;
            case InstrumentEnum.Instrument.Organ:
                selectedPlant = pipeorganNew;
                break;
            case InstrumentEnum.Instrument.Guitar:
                selectedPlant = guitarNew;
                break;
            case InstrumentEnum.Instrument.Drums:
                selectedPlant = drumkitNew;
                break;
            case InstrumentEnum.Instrument.Bass:
                selectedPlant = bassNew;
                break;
        }

        if(pos.x > 3 && pos.x < 12)
        {
            if(pos.y == 1 || pos.y == 2 || pos.y == 6 || pos.y == 7)
            {
                int[] rows = { 1, 2, 6, 7 };
                foreach (int i in rows)
                {
                    switch (GetTileName(new Vector3Int(pos.x, i, 0)))
                    {
                        case "PianoNew":
                        case "PianoGrown":
                            if(selectedPlant == pianoNew)
                                duplicate = true;
                            break;
                        case "PipeorganNew":
                        case "PipeorganGrown":
                            if(selectedPlant == pipeorganNew)
                                duplicate = true;
                            break;
                        case "GuitarNew":
                        case "GuitarGrown":
                            if(selectedPlant == guitarNew)
                                duplicate = true;
                            break;
                        case "DrumkitNew":
                        case "DrumkitGrown":
                            if(selectedPlant == drumkitNew)
                                duplicate = true;
                            break;
                        case "BassNew":
                        case "BassGrown":
                            if(selectedPlant == bassNew)
                                duplicate = true;
                            break;
                    }
                }
                if (!duplicate)
                {
                    tilemaps[pos.y].SetTile(pos, selectedPlant);
                    MusicHandler.Instance.AddPlant(pos.x - 3, false, instrument);
                }
            }
        }
        else if (pos.x == 1)
        {
            if(pos.y == 3 || pos.y == 4 || pos.y == 5)
            {
                int[] rows = {3, 4, 5};
                foreach (int i in rows)
                {
                    switch (GetTileName(new Vector3Int(pos.x, i, 0)))
                    {
                        case "PianoNew":
                        case "PianoGrown":
                            if(selectedPlant == pianoNew)
                                duplicate = true;
                            break;
                        case "PipeorganNew":
                        case "PipeorganGrown":
                            if(selectedPlant == pipeorganNew)
                                duplicate = true;
                            break;
                        case "GuitarNew":
                        case "GuitarGrown":
                            if(selectedPlant == guitarNew)
                                duplicate = true;
                            break;
                        case "DrumkitNew":
                        case "DrumkitGrown":
                            if(selectedPlant == drumkitNew)
                                duplicate = true;
                            break;
                        case "BassNew":
                        case "BassGrown":
                            if(selectedPlant == bassNew)
                                duplicate = true;
                            break;
                    }
                }
                if (!duplicate) {
                    tilemaps[pos.y].SetTile(pos, selectedPlant);
                    MusicHandler.Instance.AddPlant(9, false, instrument);
                }
            }
        }

        if (!duplicate) {
            Uproot(pos);

            if(pos.x > 3 && pos.x < 12){
                if(pos.y == 1 || pos.y == 2 || pos.y == 6 || pos.y == 7){
                    tilemaps[pos.y].SetTile(pos, selectedPlant);
                    MusicHandler.Instance.AddPlant(pos.x - 3, false, instrument);
                }
            }else if(pos.x == 1){
                if(pos.y == 3 || pos.y == 4 || pos.y == 5){
                    tilemaps[pos.y].SetTile(pos, selectedPlant);
                    MusicHandler.Instance.AddPlant(9, false, instrument);
                }
            }
        }
    }

    public void Water(Vector3Int pos){
        TileBase selectedPlant = null;
        string currentPlant = GetTileName(pos);
        InstrumentEnum.Instrument instrument = InstrumentEnum.Instrument.Piano;

        switch (currentPlant)
        {
            case "PianoNew":
                selectedPlant = pianoGrown;
                instrument = InstrumentEnum.Instrument.Piano;
                break;
            case "PipeorganNew":
                selectedPlant = pipeorganGrown;
                instrument = InstrumentEnum.Instrument.Organ;
                break;
            case "GuitarNew":
                selectedPlant = guitarGrown;
                instrument = InstrumentEnum.Instrument.Guitar;
                break;
            case "DrumkitNew":
                selectedPlant = drumkitGrown;
                instrument = InstrumentEnum.Instrument.Drums;
                break;
            case "BassNew":
                selectedPlant = bassGrown;
                instrument = InstrumentEnum.Instrument.Bass;
                break;
        }

        if(selectedPlant == null) {
            return;
        }

        Uproot(pos);

        if(pos.x > 3 && pos.x < 12){
            if(pos.y == 1 || pos.y == 2 || pos.y == 6 || pos.y == 7){
                tilemaps[pos.y].SetTile(pos, selectedPlant);
                MusicHandler.Instance.AddPlant(pos.x - 3, true, instrument);
            }
        }else if(pos.x == 1){
            if(pos.y == 3 || pos.y == 4 || pos.y == 5){
                tilemaps[pos.y].SetTile(pos, selectedPlant);
                MusicHandler.Instance.AddPlant(9, true, instrument);
            }
        }
    }

    // removes the plant at position pos if one is there
    public void Uproot(Vector3Int pos){
        if(pos.x > 3 && pos.x < 12){
            if(pos.y == 1 || pos.y == 2 || pos.y == 6 || pos.y == 7){
                InstrumentEnum.Instrument instrument = InstrumentEnum.Instrument.Piano;
                switch (GetTileName(pos))
                {
                    case "PianoNew":
                    case "PianoGrown":
                        instrument = InstrumentEnum.Instrument.Piano;
                        break;
                    case "PipeorganNew":
                    case "PipeorganGrown":
                        instrument = InstrumentEnum.Instrument.Organ;
                        break;
                    case "GuitarNew":
                    case "GuitarGrown":
                        instrument = InstrumentEnum.Instrument.Guitar;
                        break;
                    case "DrumkitNew":
                    case "DrumkitGrown":
                        instrument = InstrumentEnum.Instrument.Drums;
                        break;
                    case "BassNew":
                    case "BassGrown":
                        instrument = InstrumentEnum.Instrument.Bass;
                        break;
                    default:
                        return; // in this case there is nothing planted on this spot
                }
                tilemaps[pos.y].SetTile(pos, null);
                MusicHandler.Instance.RemovePlant(pos.x - 3, instrument);
            }
        }

        else if(pos.x == 1){
            if(pos.y == 3 || pos.y == 4 || pos.y == 5){
                InstrumentEnum.Instrument instrument = InstrumentEnum.Instrument.Piano;
                switch (GetTileName(pos))
                {
                    case "PianoNew":
                    case "PianoGrown":
                        instrument = InstrumentEnum.Instrument.Piano;
                        break;
                    case "PipeorganNew":
                    case "PipeorganGrown":
                        instrument = InstrumentEnum.Instrument.Organ;
                        break;
                    case "GuitarNew":
                    case "GuitarGrown":
                        instrument = InstrumentEnum.Instrument.Guitar;
                        break;
                    case "DrumkitNew":
                    case "DrumkitGrown":
                        instrument = InstrumentEnum.Instrument.Drums;
                        break;
                    case "BassNew":
                    case "BassGrown":
                        instrument = InstrumentEnum.Instrument.Bass;
                        break;
                    default:
                        return; // in this case there is nothing planted on this spot
                }
                tilemaps[pos.y].SetTile(pos, null);
                MusicHandler.Instance.RemovePlant(9, instrument);
            }
        }
    }
}
