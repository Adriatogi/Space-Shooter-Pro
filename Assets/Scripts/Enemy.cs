using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
        if(_animator == null)
        {
            Debug.LogError("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move enemy down
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        //Re-spawn at top
        if (transform.position.y < -5.0f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        //Enemy colliders
        if (other.gameObject.tag == "Player")
        {
            //Damage player
            if (_player != null) // Error handling
            {
                _player.Damage();
            }

            _animator.SetTrigger("onEnemyDeath");
            _speed = 0.3f;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.35f);
        } 
        else if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.addScore(10);
            }

            _animator.SetTrigger("onEnemyDeath");
            _speed = 0.3f;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 2.35f);
        }
    }
}
