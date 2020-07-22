using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour
{
    CharacterController playerCol;
    float originalHeight;
    public float reducedHeight;


    // Start is called before the first frame update
    void Start()
    {
        playerCol = GetComponent<CharacterController>();
        originalHeight = playerCol.height;
    }

    // Update is called once per frame
    void Update()
    {
        //Crouch;
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Crouch();
        else if(Input.GetKeyUp(KeyCode.LeftControl))
            GoUp();
    }

    //Method to reduce height

    void Crouch()
    {
        playerCol.height = reducedHeight;
    }

    //Method to reset height
    void GoUp()
    {
        playerCol.height = originalHeight;
    }
}
