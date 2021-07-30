using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; } // Access the functions here
    
    public float reduced_volume_level = 0.75f;

    [FMODUnity.EventRef]
	public string MusicEvent = "";

    private FMOD.Studio.EventInstance music;

    private void Awake()
    {
        if (Instance == null) // Singleton setup - only one of this game object is allowed at a time
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            music = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
            music.start();
        }
        else
        {
            {
                Destroy(gameObject); 
            }
        }

    }

    public void AddPlant(int position, bool grown, InstrumentEnum.Instrument instrument) {
        float volume;
        if (grown) {
            volume = 1.0f;
        } else {
            volume = reduced_volume_level;
        }

        SetPlantVolume(position, instrument, volume);
    }

    public void RemovePlant(int position, InstrumentEnum.Instrument instrument) {
        SetPlantVolume(position, instrument, 0.0f);
    }

    private void SetPlantVolume(int position, InstrumentEnum.Instrument instrument, float volume) {
        string param_string = "";

        switch (instrument)
        {
            case InstrumentEnum.Instrument.Piano:
                param_string += "Piano";
                break;
            case InstrumentEnum.Instrument.Organ:
                param_string += "Organ";
                break;
            case InstrumentEnum.Instrument.Guitar:
                param_string += "Guitar";
                break;
            case InstrumentEnum.Instrument.Drums:
                param_string += "Drums";
                break;
            case InstrumentEnum.Instrument.Bass:
                param_string += "Bass";
                break;
            default:
                throw new System.IndexOutOfRangeException("int position must be 1-9 (1-8 for normal slots, 9 for foundation slots)");
        }

        param_string += "Vol" + position.ToString();

        music.setParameterByName(param_string, volume);
    }
}
