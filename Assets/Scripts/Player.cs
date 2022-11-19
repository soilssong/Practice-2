using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    public float moveSpeed;
    public Rigidbody2D rb;
    public static bool isMoving = false;

    Vector3 movement;
    public delegate void Animation(string animation_type, float x,float y , bool a);
    private bool _isPlayerInputDisabled = false;
    public bool isPlayerInputDisabled { get => _isPlayerInputDisabled; set => _isPlayerInputDisabled = value; }

    public static event Animation changeRightLeftUpDown;
    private Camera maincamera;

    protected override void Awake()
    {
        base.Awake();
        maincamera = Camera.main;
    }

    void Update()
    {
        //IsRunning();

        if (!isPlayerInputDisabled)
        {
            IsWalking();
        }
       
    

       
    }

    void IsWalking()
    {
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector3.zero)
        {
            isMoving = true;
            MoveCharacter();
        }
        else { isMoving = false; MoveCharacter(); }
           
    }
    void MoveCharacter()
    {
        moveSpeed = 10f;
      
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (changeRightLeftUpDown != null)
        {
            changeRightLeftUpDown("Walking", movement.x, movement.y, isMoving);

            
        }

        
    }
                 
     public Vector3 GetPlayerViewPortPosition()
     {
      return maincamera.WorldToViewportPoint(transform.position);
     }

    public void ResetMovement()
    {
        movement.x = 0f;
        movement.y = 0f;
        isMoving = false;
        
    } 
    public void DisablePlayerInput()
    {
        isPlayerInputDisabled = true;
    }
    public void EnablePlayerInput()
    {
        isPlayerInputDisabled = false;
    }

    public void DisablePlayerInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();
        if (changeRightLeftUpDown != null)
        {
            changeRightLeftUpDown("Walking", movement.x, movement.y, isMoving);


        }
    }
   


}
