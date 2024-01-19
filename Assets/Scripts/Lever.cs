using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lever : MonoBehaviour
{
    [Header("Levers")]
    public GameObject visibleLever;
    public GameObject invisibleLever;

    [Header("Platform")]
    public GameObject platform;

    [Header("Settings")]
    public float platformLowerDistance = 3f;
    public float moveSpeed = 2.0f;

    private Renderer objectRenderer;
    private bool isLeverActivated = false;
    private bool isMoving = false;
    private bool isMovingDown = false;

    void Start()
    {
        invisibleLever.SetActive(false);
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = true;
    }

    void Update()
    {
        if (isLeverActivated && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                TogglePlatformMovement();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")) && !isLeverActivated)
        {
            isLeverActivated = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")) && isLeverActivated)
        {
            isLeverActivated = false;
        }
    }


    void TogglePlatformMovement()
    {
        if (isMovingDown)
        {
            StartCoroutine(MovePlatformUp());
            invisibleLever.SetActive(false);
            objectRenderer.enabled = !objectRenderer.enabled;

        }
        else
        {
            StartCoroutine(MovePlatformDown());
            invisibleLever.SetActive(true);
            objectRenderer.enabled = !objectRenderer.enabled;
        }
    }

    IEnumerator MovePlatformDown()
    {
        isMoving = true;
        isMovingDown = true;
        Vector3 startPosition = platform.transform.position;
        Vector3 endPosition = startPosition - new Vector3(0, platformLowerDistance, 0);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;

            yield return null;
        }

        platform.transform.position = endPosition;
        isMoving = false;
    }

    IEnumerator MovePlatformUp()
    {
        isMoving = true;
        isMovingDown = false;
        Vector3 startPosition = platform.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, platformLowerDistance, 0);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;

            yield return null;
        }

        platform.transform.position = endPosition;
        isMoving = false;
    }

}
