 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    //variable for speed
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    [SerializeField]
    private AudioClip _Clip;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //move enemy down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //when off the screen on the bottom
        if (transform.position.y < -7)
        {
            float _RandomX = Random.Range(-7, 7);
            transform.position = new Vector3(_RandomX, 7, 0);
        }
        DestroyOnEnd();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Lazer")
        {
           
            Destroy(other.gameObject);
            
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            
            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }
            AudioSource.PlayClipAtPoint(_Clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_Clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            

        }
        
    }

    private void DestroyOnEnd()
    {
        if (_gameManager.GameOver)
        {
            Destroy(this.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        }

    }
}
