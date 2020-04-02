using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private refrences
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _tripleShotCoolDown = 5.0f;
    [SerializeField]
    private float _speedBoostCoolDown = 5.0f;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _tripleShot = false;
    [SerializeField]
    private bool _shield = false;
    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject[] _engineFailures;

    private UIManager _UIManager;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserAudio;

    

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position, and set it to new position of 0,0,0
        transform.position = new Vector3(0, 0, 0);

        //Game Object handler
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        //Null checking
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }

        if(_UIManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        //Space key and fire rate
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > _canFire))
        {
            fireLaser();
        }
    }

    void fireLaser()
    {
        _canFire = Time.time + _fireRate;

        //Different type of lasers
        if (_tripleShot == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();

    }

    void calculateMovement()
    {
        //control input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //movement of player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        // X-axis movement wrap
        if (transform.position.x < -10.263f)
        {
            transform.position = new Vector3(10.263f, transform.position.y, 0);
        }
        else if (transform.position.x > 10.263f)
        {
            transform.position = new Vector3(-10.263f, transform.position.y, 0);
        }

        //Y-axis movement restrain
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.838f, 5.95f), 0);
        
    }

    public void Damage()
    {
        if (_shield == true)
        {
            //Set to false and end function for damage
            _shieldVisualizer.SetActive(false);

            _shield = false;
            return;
        }

        //Subtract and update lives
        _lives--;
        _UIManager.updateLives(_lives);

        //Random Engine Failure
        int randomEngine = Random.Range(0, 2);
        _engineFailures[randomEngine].SetActive(true);

        if (_lives < 1)
        {
            //kill player
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void tripleShotActive()
    {
        _tripleShot = true;
        StartCoroutine(tripleShotPowerDownRoutine());
    }

    IEnumerator tripleShotPowerDownRoutine()
    {
            //Wait
            yield return new WaitForSeconds(_tripleShotCoolDown);

            //Deactivate triple shot
            _tripleShot = false;
    }
    public void speedBoostActive()
    {
        _speed = _speed * _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        //Wait
        yield return new WaitForSeconds(_speedBoostCoolDown);

        //Deactivate Speed Boost
        _speed = _speed / _speedMultiplier;
    }

    public void ShieldActive()
    {
        _shieldVisualizer.SetActive(true);
        _shield = true;
    }

    public void addScore(int points)
    {
        _score += points;
        _UIManager.updateScore(_score);
    }

}
