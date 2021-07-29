using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; } // Access the functions here
    
    public float reduced_volume_level = 0.75f;

    private FMOD.Studio.EventInstance music;

    private void Awake()
    {
        if (Instance == null) // Singleton setup - only one of this game object is allowed at a time
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            music = FMODUnity.RuntimeManager.CreateInstance(FootstepsEvent);
        }
        else
        {
            {
                Destroy(gameObject); 
            }
        }

    }

     public void AddPlant(int position, bool grown, InstrumentEnum.Instrument instrument) { // I swear I'm not YandereDev I just have yet to learn a better approach with my limited knowledge of FMOD lol
        /* float volume;
        if (grown) {
            volume = 1.0f;
        } else {
            volume = reduced_volume_level;
        }

        switch (instrument)
        {
            case InstrumentEnum.Instrument.Piano:
                ChangePiano(position, volume);
                break;
            case InstrumentEnum.Instrument.Organ:
                ChangeOrgan(position, volume);
                break;
            case InstrumentEnum.Instrument.Guitar:
                ChangeGuitar(position, volume);
                break;
            case InstrumentEnum.Instrument.Drums:
                ChangeDrums(position, volume);
                break;
            case InstrumentEnum.Instrument.Bass:
                ChangeBass(position, volume);
                break;
        }*/
    }

    public void RemovePlant(int position, bool grown, InstrumentEnum.Instrument instrument) {

    }
    
    /*
    private void ChangePiano(int position, float volume) {
        switch(position) {
            case 1:
                music.setParameterByName("PianoVol1", volume);
                break;
            case 2:
                music.setParameterByName("PianoVol2", volume);
                break;
            case 3:
                music.setParameterByName("PianoVol3", volume);
                break;
            case 4:
                music.setParameterByName("PianoVol4", volume);
                break;
            case 5:
                music.setParameterByName("PianoVol5", volume);
                break;
            case 6:
                music.setParameterByName("PianoVol6", volume);
                break;
            case 7:
                music.setParameterByName("PianoVol7", volume);
                break;
            case 8:
                music.setParameterByName("PianoVol8", volume);
                break;
            case 9:
                music.setParameterByName("PianoVol9", volume);
                break;
        }
    }

    private void ChangePiano(int position, float volume) {
        switch(position) {
            case 1:
                music.setParameterByName("PianoVol1", volume);
                break;
            case 2:
                music.setParameterByName("PianoVol2", volume);
                break;
            case 3:
                music.setParameterByName("PianoVol3", volume);
                break;
            case 4:
                music.setParameterByName("PianoVol4", volume);
                break;
            case 5:
                music.setParameterByName("PianoVol5", volume);
                break;
            case 6:
                music.setParameterByName("PianoVol6", volume);
                break;
            case 7:
                music.setParameterByName("PianoVol7", volume);
                break;
            case 8:
                music.setParameterByName("PianoVol8", volume);
                break;
            case 9:
                music.setParameterByName("PianoVol9", volume);
                break;
        }
    }

    private void ChangePiano(int position, float volume) {
        switch(position) {
            case 1:
                music.setParameterByName("PianoVol1", volume);
                break;
            case 2:
                music.setParameterByName("PianoVol2", volume);
                break;
            case 3:
                music.setParameterByName("PianoVol3", volume);
                break;
            case 4:
                music.setParameterByName("PianoVol4", volume);
                break;
            case 5:
                music.setParameterByName("PianoVol5", volume);
                break;
            case 6:
                music.setParameterByName("PianoVol6", volume);
                break;
            case 7:
                music.setParameterByName("PianoVol7", volume);
                break;
            case 8:
                music.setParameterByName("PianoVol8", volume);
                break;
            case 9:
                music.setParameterByName("PianoVol9", volume);
                break;
        }
    }

    private void AddPiano(int position, float volume) {
        switch(position) {
            case 1:
                music.setParameterByName("PianoVol1", volume);
                break;
            case 2:
                music.setParameterByName("PianoVol2", volume);
                break;
            case 3:
                music.setParameterByName("PianoVol3", volume);
                break;
            case 4:
                music.setParameterByName("PianoVol4", volume);
                break;
            case 5:
                music.setParameterByName("PianoVol5", volume);
                break;
            case 6:
                music.setParameterByName("PianoVol6", volume);
                break;
            case 7:
                music.setParameterByName("PianoVol7", volume);
                break;
            case 8:
                music.setParameterByName("PianoVol8", volume);
                break;
            case 9:
                music.setParameterByName("PianoVol9", volume);
                break;
        }
    }

    private void AddPiano(int position, float volume) {
        switch(position) {
            case 1:
                music.setParameterByName("PianoVol1", volume);
                break;
            case 2:
                music.setParameterByName("PianoVol2", volume);
                break;
            case 3:
                music.setParameterByName("PianoVol3", volume);
                break;
            case 4:
                music.setParameterByName("PianoVol4", volume);
                break;
            case 5:
                music.setParameterByName("PianoVol5", volume);
                break;
            case 6:
                music.setParameterByName("PianoVol6", volume);
                break;
            case 7:
                music.setParameterByName("PianoVol7", volume);
                break;
            case 8:
                music.setParameterByName("PianoVol8", volume);
                break;
            case 9:
                music.setParameterByName("PianoVol9", volume);
                break;
        }
    } */
}
