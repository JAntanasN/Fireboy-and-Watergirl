using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watergirl : MonoBehaviour
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

    [Header("BlueDiamonds - script")]
    private BlueDiamonds blueDiamonds;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        blueDiamonds = GameObject.FindObjectOfType<BlueDiamonds>();
    }


    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (FireDoor.GetComponent<FireDoor>().isActivated && WaterDoor.GetComponent<WaterDoor>().isActivated)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Fireboy"))
        {
            // Disable collisions between the two cubes
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            // Allow Fireboy to push the box
            rb.AddForce(Vector2.right * Move * pushingForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("WaterDoors"))
        {
            WaterDoor.GetComponent<WaterDoor>().ActivateDoor();
            return;
        }

        if (collision.gameObject.CompareTag("RedDiamond"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("BlueDiamond"))
        {
            collision.gameObject.SetActive(false);
            blueDiamonds.CollectDiamond();
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
