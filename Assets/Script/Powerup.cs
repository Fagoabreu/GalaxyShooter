using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed=3f;
    [SerializeField]
    private int powerupId=0;
    [SerializeField]
    private AudioClip _clip = null;
 
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (powerupId == 0)
                {
                    player.TripleShotPowerOn();
                }else if (powerupId == 1)
                {
                    player.SpeedBoostPowerOn();
                }
                else if (powerupId == 2)
                {
                    player.EnableShields();
                }
            }
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position,0.5f);
            Destroy(this.gameObject);
        }
    }
}
