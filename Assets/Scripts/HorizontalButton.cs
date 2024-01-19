using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalButton : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;
    public Transform platformLeftPosition;
    public Transform platformRightPosition;

    [Header("Settings")]
    public float moveSpeed = 2.0f;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")))
        {
            if (!isMovingLeft && !isMovingRight)
            {
                StartCoroutine(MovePlatformLeft());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl")))
        {
            if (!isMovingLeft && !isMovingRight)
            {
                StartCoroutine(MovePlatformRight());
            }
        }
    }

    IEnumerator MovePlatformLeft()
    {
        isMovingLeft = true;
        Vector3 startPosition = platform.transform.position;

        if (platformLeftPosition != null) 
        {
            Vector3 endPosition = platformLeftPosition.position;

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                if (platform != null) 
                {
                    platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
                    elapsedTime += Time.deltaTime * moveSpeed;
                }
                else
                {
                    yield break;
                }

                yield return null;
            }

            platform.transform.position = endPosition;
        }

        isMovingLeft = false;
    }

    IEnumerator MovePlatformRight()
    {
        isMovingRight = true;
        Vector3 startPosition = platform.transform.position;

        if (platformRightPosition != null) 
        {
            Vector3 endPosition = platformRightPosition.position;

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                if (platform != null) 
                {
                    platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
                    elapsedTime += Time.deltaTime * moveSpeed;
                }
                else
                {
                    yield break;
                }

                yield return null;
            }

            platform.transform.position = endPosition;
        }

        isMovingRight = false;
    }
}
