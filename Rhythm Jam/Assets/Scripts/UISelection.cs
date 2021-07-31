using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelection : MonoBehaviour
{
    public float HeightOne;
    public float HeightTwo;
    public float HeightThree;
    public float HeightFour;
    public float HeightFive;

    private RectTransform rect;

    void Awake() {
        rect = GetComponent<RectTransform>();
        rect.position = new Vector3(rect.position.x, HeightOne, rect.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            rect.position = new Vector3(rect.position.x, HeightOne, rect.position.z);
        if(Input.GetKey(KeyCode.Alpha2))
            rect.position = new Vector3(rect.position.x, HeightTwo, rect.position.z);
        if(Input.GetKey(KeyCode.Alpha3))
            rect.position = new Vector3(rect.position.x, HeightThree, rect.position.z);
        if(Input.GetKey(KeyCode.Alpha4))
            rect.position = new Vector3(rect.position.x, HeightFour, rect.position.z);
        if(Input.GetKey(KeyCode.Alpha5))
            rect.position = new Vector3(rect.position.x, HeightFive, rect.position.z);
    }
}
