using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private PowerUps powerUpIdD;
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
        if (transform.position.y < -5.76f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Handle for player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                //PowerUp Handler
                switch (powerUpIdD)
                {
                    case PowerUps.tripleShot:
                        //Activate triple shot
                        player.tripleShotActive();
                        break;
                    case PowerUps.speedBoost:
                        //Activate speed boost
                        player.speedBoostActive();
                        break;
                    case PowerUps.shield:
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

    private enum PowerUps{
        tripleShot,
        speedBoost,
        shield
}
}