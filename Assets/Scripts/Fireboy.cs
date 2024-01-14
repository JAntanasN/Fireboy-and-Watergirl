using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fireboy : MonoBehaviour
{
    [Header("Speed")]
    public float speed;

    [Header("JumpingForce")]
    public float jumpForce;

    [Header("Pushing Force")]
    public float pushingForce;

    [Header("Doors")]
    public GameObject WaterDoor;
    public GameObject FireDoor;

    private float Move;
    private bool isGrounded;

    [Header("RedDiamonds - script")]
    private RedDiamonds redDiamonds;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        redDiamonds = GameObject.FindObjectOfType<RedDiamonds>();

    }


    void Update()
    {
        Move = Input.GetAxis("Horizontal2");

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump2") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        transform.rotation = Quaternion.Euler(Vector3.zero); // player does not trip

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Watergirl"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("WaterDoors"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            rb.AddForce(Vector2.right * Move * pushingForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("FireDoors"))
        {
            FireDoor.GetComponent<FireDoor>().ActivateDoor();
            return;
        }

        if (collision.gameObject.CompareTag("BlueDiamond"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("RedDiamond"))
        {
            collision.gameObject.SetActive(false);
            redDiamonds.CollectDiamond();
        }
    }


   

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

   


}
