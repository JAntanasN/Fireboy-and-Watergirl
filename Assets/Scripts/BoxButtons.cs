using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButtons : MonoBehaviour
{
    [Header("Platform")]
    public GameObject platform;
    public Transform platformLowerPosition;
    

    [Header("Settings")]
    public float moveSpeed = 2.0f;

    private bool isMovingDown = false;
    private bool isMovingUp = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Box")))
        {
            if (!isMovingDown && !isMovingUp)
            {
                if (platform != null && platform.activeSelf && Application.isPlaying) 
                {
                    StartCoroutine(MovePlatformDown());
                }
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
            if (platform != null && platform.activeSelf && Application.isPlaying)
            {
                platform.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
                elapsedTime += Time.deltaTime * moveSpeed;

                yield return null;
            }
            else
            {
                yield break;
            }
        }

        if (platform != null && platform.activeSelf && Application.isPlaying) 
        {
            platform.transform.position = endPosition;
        }

        isMovingDown = false;
    }
}
