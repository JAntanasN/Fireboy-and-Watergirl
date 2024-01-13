using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;
    public Transform platformLowerPosition;
    public Transform platformUpperPosition;

    [Header("Settings")]
    public float moveSpeed = 2.0f;

    private bool isMovingDown = false;
    private bool isMovingUp = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")))
        {
            // Only start moving down if the platform is not already moving
            if (!isMovingDown && !isMovingUp)
            {
                StartCoroutine(MovePlatformDown());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")))
        {
            // Only start moving up if the platform is not already moving
            if (!isMovingDown && !isMovingUp)
            {
                StartCoroutine(MovePlatformUp());
            }
        }
    }

    IEnumerator MovePlatformDown()
    {
        isMovingDown = true;
        Vector3 startPosition = platform.transform.position;
        Vector3 endPosition = platformLowerPosition.position;

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;

            yield return null;
        }

        platform.transform.position = endPosition;
        isMovingDown = false;
    }

    IEnumerator MovePlatformUp()
    {
        isMovingUp = true;
        Vector3 startPosition = platform.transform.position;
        Vector3 endPosition = platformUpperPosition.position;

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;

            yield return null;
        }

        platform.transform.position = endPosition;
        isMovingUp = false;
    }
}
