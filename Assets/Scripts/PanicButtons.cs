using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanicButtons : MonoBehaviour
{
    void OnValidate()
    {
        if (!this.enabled)
        { this.enabled = true; }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.R))
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }
}
