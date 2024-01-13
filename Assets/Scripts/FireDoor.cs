using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDoor : MonoBehaviour
{
    public bool isActivated = false;

    public void ActivateDoor()
    {
        isActivated = true;
    }
}
