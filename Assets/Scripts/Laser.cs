using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Speed variable
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            moveUp();
        }
        else
        {
            moveDown();
        }

        checkOffScreen();
    }

    private void moveUp()
    {
        //Shoot laser up
        transform.Translate(Vector3.up * Time.deltaTime * _speed);

    }

    private void moveDown()
    {
        //Shoot laser up
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
    }

    private void checkOffScreen()
    {
        //Destroy laser when it leaves the screen
        if (transform.position.y > 7f || transform.position.y < -5.0f)
        {
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void assignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}


