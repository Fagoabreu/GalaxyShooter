using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ControlsInput;

public class GameManager : MonoBehaviour, IMenuActions
{
    
    [SerializeField]
    private GameObject player = null;

    public bool gameOver = true;
    private UIManager _uiManager;
    private ControlsInput _playerControls;

    private void Awake()
    {
        _uiManager = GameObject.FindObjectOfType<Canvas>().GetComponent<UIManager>();
        
        _playerControls = new ControlsInput();
        _playerControls.Menu.SetCallbacks(this);
    }

    private void Update()
    {
      
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gameOver == true)
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.hideTitleScreen();
            }
            
        }
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

}
