using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel1Scene : MonoBehaviour
{
    public void SceneStart()
    {
        SceneManager.LoadScene("Level1");
    }
}
