using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitsChecker : MonoBehaviour
{
    [Header("Respawn Points")]
    public Transform fireboySpawnPoint;
    public Transform watergirlSpawnPoint;

    [Header("FireExplosion")]
    public GameObject FireExplosion;
    public float firelifetime = 3;

    [Header("WaterExplosion")]
    public GameObject WaterExplosion;
    public float waterlifetime = 3;

    [Header("PoisonExplosion")]
    public GameObject PoisonExplosion;
    public float poisonlifetime = 3;


    void RespawnBoth(GameObject fireboy, GameObject watergirl)
    {
        
        fireboy.transform.position = fireboySpawnPoint.position;

        
        watergirl.transform.position = watergirlSpawnPoint.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water") && gameObject.CompareTag("Fireboy"))
        {
            ExplosionWater();
            RespawnBoth(gameObject, GameObject.FindGameObjectWithTag("Watergirl"));
        }

        if (collision.gameObject.CompareTag("Fire") && gameObject.CompareTag("Watergirl"))
        {
            ExplosionFire();
            RespawnBoth(GameObject.FindGameObjectWithTag("Fireboy"), gameObject);
        }
        if (collision.gameObject.CompareTag("Poison"))
        {
            ExplosionPoison();
            RespawnBoth(GameObject.FindGameObjectWithTag("Fireboy"), GameObject.FindGameObjectWithTag("Watergirl"));
        }
    }


    private void ExplosionFire()
    {
        Instantiate(FireExplosion, transform.position, Quaternion.identity);
    }

    private void ExplosionWater()
    {
        Instantiate(WaterExplosion, transform.position, Quaternion.identity);
    }

    private void ExplosionPoison()
    {
        Instantiate(PoisonExplosion, transform.position, Quaternion.identity);
    }
}
