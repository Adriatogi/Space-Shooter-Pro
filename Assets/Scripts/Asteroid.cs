using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            //Explosion
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            //Start spawning
            _spawnManager.startSpawning();

            //Destroy laser and asteroid
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.15f);
        }
    }
}
