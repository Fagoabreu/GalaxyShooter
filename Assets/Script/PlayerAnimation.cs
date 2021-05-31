using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour, ControlsInput.IShipActions
{

    private Animator _anim=null;
    private ControlsInput _playerControls;

    Vector2 _direction;
    public void OnFire(InputAction.CallbackContext context)
    {
        
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerControls = new ControlsInput();
        _playerControls.Ship.SetCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(_direction.x < 0f)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (_direction.x > 0f)
        {
            _anim.SetBool("TurnRight",true);
            _anim.SetBool("TurnLeft", false);
        }
        else
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }
    }
    
}
