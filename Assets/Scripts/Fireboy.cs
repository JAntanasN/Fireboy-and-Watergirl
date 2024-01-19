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
    private bool isMoving;

    [Header("RedDiamonds - script")]
    private RedDiamonds redDiamonds;

    [Header("Walking Sound")]
    public AudioClip walkingSound;
    private AudioSource audioSource;

    [Header("Jump Sound")]
    public AudioClip jumpingSound;
    private AudioSource jumpAudioSource;

    [Header("DiamondCollect Sound")]
    public AudioClip CollectingSound;
    private AudioSource CollectAudioSource;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        redDiamonds = GameObject.FindObjectOfType<RedDiamonds>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkingSound;
        audioSource.loop = true; 
        audioSource.playOnAwake = false;

        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.clip = jumpingSound;
        jumpAudioSource.playOnAwake = false;

        CollectAudioSource = gameObject.AddComponent<AudioSource>();
        CollectAudioSource.clip = CollectingSound;
        CollectAudioSource.playOnAwake = false;
    }


    void Update()
    {
        Move = Input.GetAxis("Horizontal2");

        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        isMoving = Mathf.Abs(Move) > 0.1f && isGrounded;

       
        if (isMoving && !audioSource.isPlaying)
        {
            PlayWalkingSound();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (Input.GetButtonDown("Jump2") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayJumpingSound();

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

        if (collision.gameObject.CompareTag("WaterDoors2"))
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

        if (collision.gameObject.CompareTag("FireDoors2"))
        {
            FireDoor.GetComponent<FIreDoorLevel2>().ActivateDoor();
            return;
        }

        if (collision.gameObject.CompareTag("FireDoors3"))
        {
            FireDoor.GetComponent<FireDoorLevel3>().ActivateDoor();
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
            PlayCollectingSound();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void PlayWalkingSound()
    {
        if (audioSource != null && walkingSound != null)
        {
            audioSource.PlayOneShot(walkingSound);
        }
    }

    private void PlayJumpingSound()
    {
        if (jumpAudioSource != null && jumpingSound != null)
        {
            jumpAudioSource.PlayOneShot(jumpingSound);
        }
    }

    private void PlayCollectingSound()
    {
        if (CollectAudioSource != null && CollectingSound != null)
        {
            CollectAudioSource.PlayOneShot(CollectingSound);
        }
    }
}
