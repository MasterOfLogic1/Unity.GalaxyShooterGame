using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
    
    
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _PowerUpID;
    [SerializeField]
    private AudioClip _PowerupClip;
    //0 for tripleshot, 1 for speed boost,2 for shield
    // Start is called before the first frame update
    void Start()
        
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
     {
        Debug.Log("Collided with"+ other.name);
        if (other.tag == "Player")
        { 
            //access the player
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint( _PowerupClip, Camera.main.transform.position, 1f);

            if (player != null)
            {
                
                if (_PowerUpID == 0)
                {
                    //enable triple shot
                    player.TripleShotPowerUpOn(); }
                else if (_PowerUpID == 1)
                    {
                    //enable speed boost
                    player.SpeedBoostPowerUpON();
                }
                else if (_PowerUpID == 2)
                {
                    //enable shield
                    player.EnableSheild();

                }


            }
            
            //destroy ourself
            Destroy(this.gameObject);
        }

    }
    
        
    
}
