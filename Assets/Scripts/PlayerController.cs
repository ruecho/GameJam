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
    public float coyoteTime;
    float coyoteTimer;
    public float runAcceleration;
    public float changeDirAcceleration;
    public float maxSpeed;
    public float runDeceleration;
    float movementSpeed;
    bool active = true;
    public LayerMask lm;
    public float deathCooldown = 3f;
    float deathTimer;
    public ParticleSystem ps;
    public GameObject spriteStuff;
    bool readyToRespawn = false;
    GameObject playerSpawn;
    public AudioSource explosion;
    public AudioSource jump;
    
    void Start()
    {
        playerSpawn = FindFirstObjectByType<PlayerSpawn>().gameObject;
    }
    void Update()
    {
        if(readyToRespawn)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0) Respawn();
            return;
        }
        // jump and gravity
        if(gravitationalSpeed > maxFallingSpeed)
        {
            gravitationalSpeed -= gravityAcceleration * Time.deltaTime;
            gravitationalSpeed = Mathf.Clamp(gravitationalSpeed, maxFallingSpeed, jumpAcceleration);
        }
        coyoteTimer -= Time.deltaTime;
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            gravitationalSpeed = jumpAcceleration;
            jump.pitch = Random.Range(0.9f, 1.1f);
            jump.Play();
        }
        // walk and run
        float playerInput = Input.GetAxis("Horizontal");
        int moveDir = playerInput > 0 ? 1 : (playerInput < 0 ? -1 : 0);      
        if(moveDir != 0)
        {
            if(movementSpeed * moveDir >= 0f)
            {
                movementSpeed += runAcceleration * moveDir * Time.deltaTime;                
            }
            if(movementSpeed * moveDir < 0f)
            {
                movementSpeed += changeDirAcceleration * moveDir * Time.deltaTime;
            }
            movementSpeed = Mathf.Clamp(movementSpeed, -maxSpeed, maxSpeed);
        }
        if(moveDir == 0)
        {
            if(movementSpeed > 0)
            {
                movementSpeed -= runDeceleration * Time.deltaTime;
            }
            if(movementSpeed < 0)
            {
                movementSpeed += runDeceleration * Time.deltaTime;
            }
            if(movementSpeed < 0.9f && movementSpeed > -0.9f)
            {
                movementSpeed = 0;
            } 
        }
        // finalize movement
        Vector3 move = new Vector3(movementSpeed, gravitationalSpeed);
        move *= Time.deltaTime;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3.down * 0.25f), new Vector2(0.95f,0.5f), 0f, Vector2.down, Mathf.Abs(move.y),lm);
        if (hit.collider != null) coyoteTimer = coyoteTime;
        isGrounded = coyoteTimer > 0;
        if(hit.collider != null && move.y < 0)
        {
            move.y = -hit.distance;
            gravitationalSpeed = 0;
            if(hit.collider.gameObject.tag == "KillerObstacle")
            {
                Death();
            }
        }
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position + (Vector3.up * 0.25f), new Vector2(0.95f,0.5f), 0f, Vector2.up, Mathf.Abs(move.y),lm);
        if(hit2.collider != null && move.y > 0)
        {
            move.y = hit2.distance;
            if(!Input.GetButton("Jump"))gravitationalSpeed /= 2;
            if(hit2.collider.gameObject.tag == "KillerObstacle")
            {
                Death();
            }
        }
        RaycastHit2D hit3 = Physics2D.BoxCast(transform.position + (Vector3.left * 0.25f), new Vector2(0.5f,0.95f), 0f, Vector2.left, Mathf.Abs(move.x),lm);
        if(hit3.collider != null && move.x < 0)
        {
            move.x = -hit3.distance;
            movementSpeed = 0;
            if(hit3.collider.gameObject.tag == "KillerObstacle")
            {
                Death();
            }
        }
        RaycastHit2D hit4 = Physics2D.BoxCast(transform.position + (Vector3.right * 0.25f), new Vector2(0.5f,0.95f), 0f, Vector2.right, Mathf.Abs(move.x),lm);
        if(hit4.collider != null && move.x > 0)
        {
            move.x = -hit4.distance;
            movementSpeed = 0;
            if(hit4.collider.gameObject.tag == "KillerObstacle")
            {
                Death();
            }
        }
        RaycastHit2D hit5 = Physics2D.BoxCast(transform.position, new Vector2(1,1), 0f, Vector2.zero, 0);
        if(hit5.collider != null)
        {            
            if(hit5.collider.gameObject.tag == "KillerObstacle")
            {
                Death();
            }
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
    void Death()
    {
        explosion.Play();
        readyToRespawn = true;
        spriteStuff.SetActive(false);
        ps.Play();
        deathTimer = deathCooldown;
        transform.GetComponent<DropMaker>().spawnDropletBoom(transform.position,2.0f,5);
    }
    void Respawn()
    {
        gravitationalSpeed = 0;
        movementSpeed = 0;
        transform.position = playerSpawn.transform.position;
        readyToRespawn = false;
        spriteStuff.SetActive(true);
        ps.Stop();
    }

    public void kill()
    {
        float a = 0;// Mathf.PI;
        Vector3 spp = transform.position;
        spp.y -= 1;
        if (transform.GetComponentInParent<PlayerSpawner>() != null)
        {
            transform.GetComponentInParent<PlayerSpawner>().spawnDropletBoom(transform.position, 3.0f, 20);

            Destroy(this.gameObject);
            active = false;
        }
        else
        {
            Death();
        }
    }
}
