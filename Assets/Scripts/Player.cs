using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public static bool isMoving = false;

    Vector3 movement;
    public delegate void Animation(string animation_type, float x,float y , bool a);

    public static event Animation changeRightLeftUpDown;

    void Update()
    {
        //IsRunning();


        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        IsWalking();

       
    }

    void IsWalking()
    {
      
        if (movement != Vector3.zero)
        {
            isMoving = true;
            MoveCharacter();
        }
        else { isMoving = false; MoveCharacter(); }
           
    }
    void MoveCharacter()
    {
      
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (changeRightLeftUpDown != null)
        {
            changeRightLeftUpDown("Walking", movement.x, movement.y, isMoving);

            
        }

        
    }
    // void StopCharacter()
    //{
    //    if (changeRightLeftUpDown != null)
    //    {
    //        changeRightLeftUpDown("Walking", movement.x, movement.y, isMoving);


    //    }
    //}


    

        
    

    //void IsRunning()
    //{
    //    if (Input.GetKey(KeyCode.M))
    //    {

    //        moveSpeed = 10f;
    //        if (changeAnimation != null)
    //        {
    //            changeAnimation("Running",movement.x);
                
    //        }
    //    }

      
    //}

    

}
