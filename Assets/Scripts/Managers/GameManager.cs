using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(ModeManager))]
[RequireComponent(typeof(PanicButtons))]

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance is null)
        { instance = this; }
        else if (instance != this)
        { Destroy(gameObject); }
    }

    void Start()
    {
        transform.parent = Player.instance.Head;
        transform.localPosition = Vector3.zero;
    }

    public ScoreManager ScoreManager { get; set; }
    public LifeManager LifeManager { get; set; }
    public TimeManager TimeManager { get; set; }
    public ModeManager ModeManager { get; set; }

    public void SpawnParticleEffect(GameObject go, Vector3 location)
    {
        Instantiate(go, location, Quaternion.identity).GetComponent<ParticleSystem>().Play();
    }
}