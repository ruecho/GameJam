using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name!= "Ignore") {
            //GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            //GetComponent<Rigidbody2D>().drag = 100.0f;
            //GetComponent<Rigidbody2D>().enabled = false;
            //GetComponent<Collider2D>().enabled = false;
            // Destroy(GetComponent<Rigidbody2D>());   

        }

    }
}
