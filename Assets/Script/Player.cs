using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static ControlsInput;

public class Player : MonoBehaviour, IShipActions
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private GameObject _explosionPrefab = null;
    [SerializeField]
    private GameObject _laserPrefab=null;
    [SerializeField]
    private bool _canTrippleShot = false;
    [SerializeField]
    private GameObject _trippleShotPrefab = null;
    [SerializeField]
    private bool _shieldsActive = false;
    [SerializeField]
    private GameObject _shieldGameObject = null;
    [SerializeField]
    private GameObject[] _engines = null;
    //inputs
    private Vector3 _direction;
    private bool _fire = false;
    private int _hitCount = 0;

    //aux
    private ControlsInput _playerControls;
    private float _canFire = 0.0f;
    private bool _isSpeedBoostActive = false;

    //comunication
    private UIManager _uiManager =null;
    private GameManager _gameManager;
    private SpawnMagager _spawnManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        
        _playerControls = new ControlsInput();
        _playerControls.Ship.SetCallbacks(this);
        _uiManager = GameObject.FindObjectOfType<Canvas>().GetComponent<UIManager>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _spawnManager = GameObject.FindObjectOfType<SpawnMagager>();
        _audioSource = GetComponent<AudioSource>();

        _shieldGameObject.SetActive(false);
        _hitCount = 0;
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
        
    }

    private void Start()
    {
        if (_spawnManager)
        {
            _spawnManager.StartSpawnCorroutines();
        }
    }

    private void Update()
    {
        this.Movement();
        this.Shoot();

    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        _fire = context.performed;
    }


    private void Movement()
    {
        transform.Translate(_direction * (_isSpeedBoostActive ? _speed * 2f : _speed) * Time.deltaTime);

        float posY = transform.position.y;
        if (posY > 0)
        {
            posY = 0;
        }
        else if (posY < -4.2f)
        {
            posY = -4.2f;
        }

        float posX = transform.position.x;
        if (posX > 9.4f)
        {
            posX = -9.4f;
        }
        else if (posX < -9.4f)
        {
            posX = 9.4f;
        }

        transform.position = new Vector3(posX, posY, 0);
    }

    private void Shoot()
    {
        if (_fire && Time.time > _canFire)
        {
            _audioSource.Play();
            if (_canTrippleShot)
            {
                Instantiate(_trippleShotPrefab, transform.position , Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
            
        }
    }

    public void TripleShotPowerOn()
    {
        this._canTrippleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerOn()
    {
        this._isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void EnableShields()
    {
        if (!_shieldsActive)
        {
            this._shieldsActive = true;
            _shieldGameObject.SetActive(true);
        }
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this._canTrippleShot = false;
    }
    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this._isSpeedBoostActive = false;
    }

    public void Damage()
    {
        
        if (_shieldsActive)
        {
            _shieldsActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        _hitCount++;
        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }else if (_hitCount == 1)
        {
            _engines[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives <1)
        {
            Instantiate(_explosionPrefab, transform.position, transform.rotation);
            _gameManager.gameOver = true;
            _uiManager.showTitleScreen();
           Destroy(this.gameObject);
        }
    }

}
