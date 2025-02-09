using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool CanTripleShot = false;
    public bool IsSpeedBoostActive = false;
    public bool ShieldActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject[] _Engines;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _FireRate = 0.25f;

    private float _CanFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;

    private GameManager _gameManager;

    private SpawnManger _SpawnManager;

    private AudioSource _audiosource;

    private int _hitcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("x pos" + transform.rotation);//
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _SpawnManager = GameObject.Find("Spwan_Manger").GetComponent<SpawnManger>();
        if (_SpawnManager != null)
        {
            _SpawnManager.StartSpawnRountines();
        }
        _audiosource = GetComponent<AudioSource>(); 

        if (_uiManager != null)
        {
            _uiManager.Updatelives(lives); 
        }

        _hitcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            shoot();
        }


    }

    private void Movement ()
    {
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");
        //if speedup ouble the speed by 5.0 
        if (IsSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * 1.5f * HorizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * 1.5f * VerticalInput);
            
        }
        else {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * HorizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * VerticalInput);

        }
            
        
        //if player on y is greater than 0 
        //set player position to 0
        if (transform.position.y > 0)
        { transform.position = new Vector3(transform.position.x, 0, 0); }
        else if (transform.position.y < -4.2f)
        { transform.position = new Vector3(transform.position.x, -4.2f, 0); }

        //if player on x is greater than 9.4 
        //set player position to -9.4

        if (transform.position.x > 9.4f)
        { transform.position = new Vector3(-9.4f, transform.position.y, 0); }
        else if (transform.position.x < -9.4f)
        { transform.position = new Vector3(9.4f, transform.position.y, 0); }
    }

    public void Damage()
    {
       
        //if player has sheilds
        if (ShieldActive == true)
        {
            ShieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        //subtract one life from player
        lives--;
        _uiManager.Updatelives(lives);

        _hitcount++;
        if (_hitcount == 1)
        {
            //left engine failure
            _Engines[0].SetActive(true);
        }
        else if (_hitcount == 2)
        {
            //right engine failure
            _Engines[1].SetActive(true);
        }

        if (lives < 1) {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _gameManager.GameOver = true;
            _uiManager.ShowTitleScreen();
        }
    }

    private void shoot()
    {
        //if space key is pressed
        //spawn lazer
        
            if (Time.time > _CanFire)
            {
            _audiosource.Play();
            if (CanTripleShot == true)
               {
                Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
                }
            else
              {
                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
               }
                _CanFire = Time.time + _FireRate;

             }

        
    }
    //method to start tripleshot power up
    public void TripleShotPowerUpOn()
    {
        CanTripleShot = true;
        StartCoroutine(TripleShotPowerDownRountine());
    }

    public IEnumerator TripleShotPowerDownRountine()
    {
        yield return new WaitForSeconds(5.0f);
        CanTripleShot = false;
    }

    //method to start sheild
    public void EnableSheild()
    {
        ShieldActive = true;
        _shieldGameObject.SetActive(true);


    }

    //method to start speed up power up
    public void SpeedBoostPowerUpON()
    {
        IsSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRountine());
    }

    public IEnumerator SpeedBoostPowerDownRountine()
    { yield return new WaitForSeconds(5.0f);
        IsSpeedBoostActive = false;
    }
}
