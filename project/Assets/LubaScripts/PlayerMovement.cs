using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    private float gravity = -9.8f;
    public float jumpHeight = 3;

  
    
   
   
    void Start()
    {
        controller = GetComponent<CharacterController>();
       
       
    }



  
    



    // Update is called once per frame
    void Update()
    {
      

       

       


    }

 

   

   

    



    //input man script appl to cc
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}