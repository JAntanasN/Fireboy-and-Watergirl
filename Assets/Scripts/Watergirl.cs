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
    private bool isMoving;

    [Header("BlueDiamonds - script")]
    private BlueDiamonds blueDiamonds;

    private Rigidbody2D rb;

    [Header("Walking Sound")]
    public AudioClip walkingSound;
    private AudioSource audioSource;

    [Header("Jump Sound")]
    public AudioClip jumpingSound;
    private AudioSource jumpAudioSource;

    [Header("DiamondCollect Sound")]
    public AudioClip CollectingSound;
    private AudioSource CollectAudioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        blueDiamonds = GameObject.FindObjectOfType<BlueDiamonds>();

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
        Move = Input.GetAxis("Horizontal");

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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayJumpingSound();
        }

        transform.rotation = Quaternion.Euler(Vector3.zero);

        if (FireDoor != null && WaterDoor != null)
        {
            if (FireDoor.GetComponent<FireDoor>() != null && WaterDoor.GetComponent<WaterDoor>() != null)
            {
                if (FireDoor.GetComponent<FireDoor>().isActivated && WaterDoor.GetComponent<WaterDoor>().isActivated)
                {
                    SceneManager.LoadScene(3);
                }
            }
        }

        if (FireDoor != null && WaterDoor != null)
        {
            if (FireDoor.GetComponent<FIreDoorLevel2>() != null && WaterDoor.GetComponent<WaterDoorLevel2>() != null)
            {
                if (FireDoor.GetComponent<FIreDoorLevel2>().isActivated && WaterDoor.GetComponent<WaterDoorLevel2>().isActivated)
                {
                    SceneManager.LoadScene(4);
                }
            }
        }

        if (FireDoor != null && WaterDoor != null)
        {
            if (FireDoor.GetComponent<FireDoorLevel3>() != null && WaterDoor.GetComponent<WaterDoorLevel3>() != null)
            {
                if (FireDoor.GetComponent<FireDoorLevel3>().isActivated && WaterDoor.GetComponent<WaterDoorLevel3>().isActivated)
                {
                    SceneManager.LoadScene(5);
                }
            }
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
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            rb.AddForce(Vector2.right * Move * pushingForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("WaterDoors"))
        {
            WaterDoor.GetComponent<WaterDoor>().ActivateDoor();
            return;
        }

        if (collision.gameObject.CompareTag("WaterDoors2"))
        {
            WaterDoor.GetComponent<WaterDoorLevel2>().ActivateDoor();
            return;
        }

        if (collision.gameObject.CompareTag("WaterDoors3"))
        {
            WaterDoor.GetComponent<WaterDoorLevel3>().ActivateDoor();
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
