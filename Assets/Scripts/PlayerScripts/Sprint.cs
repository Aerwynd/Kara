using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{

    //Variables;

    PlayerMovement PlayerMovement;
    public float speedBoost = 10f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            PlayerMovement.speed += speedBoost;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            PlayerMovement.speed -= speedBoost;

    }
}
