using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitsChecker : MonoBehaviour
{
    [Header("Respawn Points")]
    public Transform fireboySpawnPoint;
    public Transform watergirlSpawnPoint;

    void RespawnBoth(GameObject fireboy, GameObject watergirl)
    {
        
        fireboy.transform.position = fireboySpawnPoint.position;

        
        watergirl.transform.position = watergirlSpawnPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water") && gameObject.CompareTag("Fireboy"))
        {
            RespawnBoth(gameObject, GameObject.FindGameObjectWithTag("Watergirl"));
        }

        if (collision.gameObject.CompareTag("Fire") && gameObject.CompareTag("Watergirl"))
        {
            RespawnBoth(GameObject.FindGameObjectWithTag("Fireboy"), gameObject);
        }
        if (collision.gameObject.CompareTag("Poison"))
        {
            RespawnBoth(GameObject.FindGameObjectWithTag("Fireboy"), GameObject.FindGameObjectWithTag("Watergirl"));
        }
    }
}
