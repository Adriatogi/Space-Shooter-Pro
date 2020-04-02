using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = tripleShot, 1 = Speed, 2 = Shield
    private int powerUpIdD;
    [SerializeField]
    private AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Destroy once off-screen
        if(transform.position.y < -5.76f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Handle for player
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                //PowerUp Handler
                switch (powerUpIdD)
                {
                    case 0:
                        //Activate triple shot
                        player.tripleShotActive();
                        break;
                    case 1:
                        //Activate speed boost
                        player.speedBoostActive();
                        break;
                    case 2:
                        //Activate shield
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            Destroy(this.gameObject);
        }
    }
}