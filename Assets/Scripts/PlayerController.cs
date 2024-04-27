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
    public float runAcceleration;
    public float maxSpeed;
    public float runDeceleration;
    float verticalSpeed;
    bool active = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // jump and gravity
        if(horizontalSpeed > maxFallingSpeed)
        {
            horizontalSpeed -= gravityAcceleration * Time.deltaTime;
            horizontalSpeed = Mathf.Clamp(horizontalSpeed, maxFallingSpeed, jumpAcceleration);
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            horizontalSpeed = jumpAcceleration;
        }
        // walk and run
        float playerInput = Input.GetAxis("Horizontal");
        bool isMoving = (playerInput != 0);
        int moveDir = playerInput > 0 ? 1 : (playerInput < 0 ? -1 : 0);
                
        if (isMoving)
        {
            verticalSpeed += runAcceleration * moveDir * Time.deltaTime;
            verticalSpeed = Mathf.Clamp(verticalSpeed, -maxSpeed, maxSpeed);
        }
        else if (verticalSpeed != 0)
        {
            verticalSpeed -= runDeceleration * (verticalSpeed > 0 ? 1 : -1) * Time.deltaTime;
            verticalSpeed = Mathf.Clamp(verticalSpeed, 0, (verticalSpeed > 0 ? 1 : verticalSpeed < 0 ? -1 : 0));
        }
        //Debug.Log(playerInput);
        // finalize movement
        Vector3 move = new Vector3(playerInput, horizontalSpeed);
        move *= Time.deltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.down * 0.25f), new Vector2(1,0.5f), 0f, Vector2.down, Mathf.Abs(move.y));
        isGrounded = (hit.collider != null);
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
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (active)
            {
                this.GetComponentInParent<PlayerSpawner>().spawnDroplet(transform.position);
                Destroy(this.gameObject);
                active = false;
            }
        }
    }
}
