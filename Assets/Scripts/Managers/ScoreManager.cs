using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class ScoreManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.ScoreManager = this;
    }
}
