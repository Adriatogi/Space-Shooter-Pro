using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Speed variable
    [SerializeField]
    private float _speed = 8.0f;
 
    // Update is called once per frame
    void Update()
    {
        //Shoot laser up
        transform.Translate(Vector3.up * Time.deltaTime * _speed);

        //Destroy laser when it leaves the screen
        if(transform.position.y > 7f)
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
}
