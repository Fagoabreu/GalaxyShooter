using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private GameObject explosionPrefab=null;

    [SerializeField]
    private int _scoreValue = 10;

    [SerializeField]
    private AudioClip _audioClip = null;

    //comunicacao
    private UIManager _uiManager = null;
   

    private void Awake()
    {
        _uiManager = GameObject.FindObjectOfType<Canvas>().GetComponent<UIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.2f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6.12f, 0);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _uiManager.UpdateScores(_scoreValue);
            this.Damage();
        }
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            this.Damage();
        }
    }

    private void Damage()
    {
        AudioSource.PlayClipAtPoint(_audioClip,Camera.main.transform.position);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
