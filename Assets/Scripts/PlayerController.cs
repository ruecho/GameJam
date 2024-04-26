using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float gravityAcceleration;
    public float maxFallingSpeed;
    public float jumpAcceleration;
    float horizontalSpeed;
    bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(horizontalSpeed > maxFallingSpeed)
        {
            horizontalSpeed -= gravityAcceleration * Time.deltaTime;
            horizontalSpeed = Mathf.Clamp(horizontalSpeed, maxFallingSpeed, jumpAcceleration);
        }
        if(Input.GetButtonDown("Jump"))
        {
            horizontalSpeed = jumpAcceleration;
        }
        // finalize movement
        Vector3 move = new Vector3(0, horizontalSpeed);
        move *= Time.deltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.down * 0.25f), new Vector2(1,0.5f), 0f, Vector2.down, Mathf.Abs(move.y));
        if(hit.collider != null && move.y < 0)
        {
            move.y = -hit.distance;
            horizontalSpeed = 0;
        }
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position + (Vector3.up * 0.25f), new Vector2(1,0.5f), 0f, Vector2.up, Mathf.Abs(move.y));
        if(hit2.collider != null && move.y > 0)
        {
            move.y = hit.distance;
            if(!Input.GetButton("Jump"))horizontalSpeed /= 2;
        }
        transform.Translate(move);
    }
}
