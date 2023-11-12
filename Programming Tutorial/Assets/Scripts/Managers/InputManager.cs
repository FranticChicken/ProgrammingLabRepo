using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    //Variable
    private static Controls _controls;

    public static void Init(Player myPlayer)
    {

        //creating a new controls
        _controls = new Controls();

        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined;

        //with our new controls we need to initialize and activate them
        _controls.Permanent.Enable();

        //binding movement controls
        _controls.Game.Movement.performed += hi =>
        {
            //hi.ReadValue<Vector3>() is a function from input manager that gives the values for what the Vector3 variable will be
            Vector3 _inputValue = hi.ReadValue<Vector3>();
            myPlayer.SetMovementDirection(_inputValue);
            
        };

        //binding jump controls
        _controls.Game.Jump.started += jumpyboy =>
        {
            myPlayer.Jump();
            

        };

        _controls.Game.Shoot.performed += shootyboy =>
        {
            myPlayer.ShootingProof();
        };


        _controls.Game.Crouch.performed += crouchyboy =>
        {
            myPlayer.CrouchProof();
        };

        _controls.Game.Look.performed += ctx =>
        {
            myPlayer.SetLookDirection(ctx.ReadValue<Vector2>()); 
        };

        _controls.Game.Shoot.performed += ctx =>
        {
            myPlayer.Shoot();
        };
    }

     

    public static void GameMode()
    {
        _controls.Game.Enable();
        _controls.UI.Disable();
    }

    public static void UIMode()
    {
        _controls.Game.Disable();
        _controls.UI.Enable();
    }


}
 