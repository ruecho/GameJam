using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float gravityAcceleration;
    public float maxFallingSpeed;
    public float jumpAcceleration;
    float gravitationalSpeed;
    bool isGrounded;
    public float runAcceleration;
    public float changeDirAcceleration;
    public float maxSpeed;
    public float runDeceleration;
    float movementSpeed;
    bool active = true;
    public LayerMask lm;
    //public int layer=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // jump and gravity
        if(gravitationalSpeed > maxFallingSpeed)
        {
            gravitationalSpeed -= gravityAcceleration * Time.deltaTime;
            gravitationalSpeed = Mathf.Clamp(gravitationalSpeed, maxFallingSpeed, jumpAcceleration);
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            gravitationalSpeed = jumpAcceleration;
        }
        // walk and run
        float playerInput = Input.GetAxis("Horizontal");
        int moveDir = playerInput > 0 ? 1 : (playerInput < 0 ? -1 : 0);      
        if(moveDir != 0)
        {
            Debug.Log("accelerate");
            if(movementSpeed * moveDir >= 0f)
            {
                Debug.Log("normal");
                movementSpeed += runAcceleration * moveDir * Time.deltaTime;                
            }
            if(movementSpeed * moveDir < 0f)
            {
                Debug.Log("changedir");
                movementSpeed += changeDirAcceleration * moveDir * Time.deltaTime;
            }
            movementSpeed = Mathf.Clamp(movementSpeed, -maxSpeed, maxSpeed);
        }
        if(moveDir == 0)
        {
            Debug.Log("decelerate");
            if(movementSpeed > 0)
            {
                Debug.Log("right");
                Debug.Log(movementSpeed);
                movementSpeed -= runDeceleration * Time.deltaTime;
                Debug.Log(movementSpeed);
            }
            if(movementSpeed < 0)
            {
                Debug.Log("left");
                movementSpeed += runDeceleration * Time.deltaTime;
            }
            if(movementSpeed < 0.02f && movementSpeed > -0.02f)
            {
                Debug.Log("stopped");
                movementSpeed = 0;
            } 
        }
        // finalize movement
        Debug.Log("final move speed:" + movementSpeed);
        Vector3 move = new Vector3(movementSpeed, gravitationalSpeed);
        Debug.Log(move);
        move *= Time.deltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.down * 0.25f), new Vector2(0.95f,0.5f), 0f, Vector2.down, Mathf.Abs(move.y),lm);
        isGrounded = (hit.collider != null);
        if(hit.collider != null && move.y < 0)
        {
            move.y = -hit.distance;
            gravitationalSpeed = 0;
        }
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position + (Vector3.up * 0.25f), new Vector2(0.95f,0.5f), 0f, Vector2.up, Mathf.Abs(move.y),lm);
        if(hit2.collider != null && move.y > 0)
        {
            move.y = hit2.distance;
            if(!Input.GetButton("Jump"))gravitationalSpeed /= 2;
        }
        RaycastHit2D hit3 = Physics2D.BoxCast(transform.position + (Vector3.left * 0.25f), new Vector2(0.5f,0.95f), 0f, Vector2.left, Mathf.Abs(move.x),lm);
        if(hit3.collider != null && move.x < 0)
        {
            move.x = -hit3.distance;
            movementSpeed = 0;
        }
        RaycastHit2D hit4 = Physics2D.BoxCast(transform.position + (Vector3.right * 0.25f), new Vector2(0.5f,0.95f), 0f, Vector2.right, Mathf.Abs(move.x),lm);
        if(hit4.collider != null && move.x > 0)
        {
            move.x = -hit4.distance;
            movementSpeed = 0;
        }
        transform.Translate(move);
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (active)
            {
                kill();
            }
        }
    }

    public void kill()
    {
        this.GetComponentInParent<PlayerSpawner>().spawnDroplet(transform.position);
        Destroy(this.gameObject);
        active = false;
    }
}
