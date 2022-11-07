using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public static bool isRunning;
    Vector2 movement;



    public delegate void Animation(string animation_type, bool a);
    public static event Animation changeAnimation;

     void Update()
    {
        IsRunning();


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);



     


        
    }

     void IsRunning()
    {
        if (Input.GetKey(KeyCode.M))
        {

            moveSpeed = 10f;
            if (changeAnimation != null)
            {
                changeAnimation("Running",true);
                
            }
        }

        else if(!Input.GetKey(KeyCode.M))
        {
            moveSpeed = 5f;
            changeAnimation("Running",false);
           
        }
      
    }

    

}
