using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToControlsScene : MonoBehaviour
{
    public void SceneStart()
    {
        SceneManager.LoadScene("Controls");
    }
}
