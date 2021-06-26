using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5;

    private float camFOV;
    private float camFOVLerpEnd;

    private bool isJumping;
    private bool canJump;

    private bool isBouncing;

    public int jumpForce = 8;

    private bool canBouncePlayer;
    private bool isLookingAtBounceObject;

    Vector3 jumpDirection;

    Vector3 bounceDirection;

    Vector3 moveVector;

    private RaycastHit hit;

    void Start()
    {
        speed = 5;
        jumpForce = 8;
    }

    private void Update()
    {
        Jump();
        //Camera.main.fieldOfView = 60 + speed * 2;
    }

    void FixedUpdate()
    {
        Move();
        BounceRayCastCheck();
    }

    void Move()
    {
        if (!isJumping)
        {
            moveVector.x = Input.GetAxisRaw("Vertical") * speed;
            moveVector.z = Input.GetAxisRaw("Horizontal") * speed;
        }
        
        rb.velocity = transform.TransformDirection(new Vector3(moveVector.z, rb.velocity.y, moveVector.x));
    }

    void Jump()
    {   
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            isJumping = true;
            canJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

            jumpDirection = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
            rb.AddForce(0, jumpForce + (speed * 0.75f), 0, ForceMode.Impulse);

            speed += 1;
        }

        if(Input.GetKeyDown(KeyCode.Space) && canBouncePlayer && isLookingAtBounceObject)
        {
            Bounce(bounceDirection);
            canBouncePlayer = false;
            speed += 1.5f;
        }

        if(isJumping)
        {
            moveVector = jumpDirection * speed * 1.5f;
        }
    }

    void BounceRayCastCheck()
    {
        //Debug.DrawRay(transform.position + new Vector3(0,1,0), transform.TransformDirection(Vector3.forward) * 10, Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.TransformDirection(Vector3.forward) * 10, out hit, 10) && hit.transform.tag == "BounceCollider")
        {
            isLookingAtBounceObject = true;
        }
        else
        {
            isLookingAtBounceObject = false;
        }
    }

    void Bounce(Vector3 colNormal)
    {
        isJumping = true;
        rb.velocity = Vector3.Reflect(transform.forward * 100, colNormal);

        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);

        transform.rotation = targetRotation;

        jumpDirection = new Vector3(1, 0, 0);

        rb.AddForce(0, jumpForce + (speed * 0.75f), 0, ForceMode.Impulse);

        canBouncePlayer = false;           
    }

    private void OnCollisionEnter(Collision collision)
    {
        //8 is the "Jumpable" layer
        if(collision.gameObject.layer == 8)
        {
            canJump = true;
            isJumping = false;
        }
        //9 is the "BounceAble" layer
        if(collision.gameObject.tag == "BounceCollider")
        {
            bounceDirection = collision.contacts[0].normal;
            canBouncePlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //9 is the "BounceAble" layer
        if (collision.gameObject.tag == "BounceCollider")
        {
            canBouncePlayer = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //8 is the "Jumpable" layer
        if (collision.gameObject.layer == 8)
        {
            if(speed > 5)
            {
                speed *= 0.98f;
            }
        }

        //9 is the "BounceAble" layer
        if (collision.gameObject.layer == 9)
        {
            if (speed > 5)
            {
                speed *= 0.5f;
            }
        }
    }
}
