using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public new Transform camera;
    public Rigidbody rb;
    public Animator animator;

    public float camRotationSpeed    = 5f;
    public float camMinimumY         = -60f;
    public float camMaximumY         = 75f;
    public float rotationSmoothSpeed = 10f;
    
    public float walkSpeed = 6f;
    public float runSpeed  = 12f;
    public float maxSpeed  = 25f;
    public float jumpPower = 1f;

    //public float extraGravity = 45;

    float   bodyRotationX;
    float   camRotationY;
    Vector3 directionIntentX;
    Vector3 directionIntentY;
    float   speed;

    public bool isGrounded = true;
    
    void Update()
    {
        LookRotation();
        Movement();
        
    }

    void LookRotation()
    {
        // Curseur verouillé au centre et invisible
       
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Recupération des valeurs de rotation de la camera et du corps

        bodyRotationX += Input.GetAxis("Mouse X") * camRotationSpeed;
        camRotationY  += Input.GetAxis("Mouse Y") * camRotationSpeed;

        // Empêcher la caméra de faire un 360°

        camRotationY = Mathf.Clamp(camRotationY, camMinimumY, camMaximumY);

        // crée une cible de rotation et gère la rotation du corps + caméra

        Quaternion camTargetRotation = Quaternion.Euler(-camRotationY, 0, 0);
        Quaternion bodyTargetRotation = Quaternion.Euler(0, bodyRotationX, 0);

        // gére les rotations

        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, Time.deltaTime * rotationSmoothSpeed);

        camera.localRotation = Quaternion.Lerp(camera.localRotation, camTargetRotation, Time.deltaTime * rotationSmoothSpeed);
    }

    void Movement()
    {
        // Direction de la caméra = direction du player

        directionIntentX = camera.right;
        directionIntentX.y = 0;
        directionIntentX.Normalize();

        directionIntentY = camera.forward;
        directionIntentY.y = 0;
        directionIntentY.Normalize();

        // Changer la velocité de notre player dans cette direction

        rb.velocity = directionIntentY * Input.GetAxis("Vertical") * speed + directionIntentX * Input.GetAxis("Horizontal") * speed + Vector3.up * rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Controle de la vitesse basé sur notre état de mouvement

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded == true )
        {
            rb.AddForce(new Vector3(0, jumpPower , 0), ForceMode.Impulse);
            isGrounded = false;
            
        }

        // Animations
        
        //Déplacement normal
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsRunning", true);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("IsRunning", false);
        }
        
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsSprinting", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsSprinting", false);
        }

        //Saut 

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            animator.SetBool("Jump", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
    }
    
    
}
