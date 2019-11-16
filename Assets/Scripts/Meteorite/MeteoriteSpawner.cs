using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnDistance = new Vector2();
    [SerializeField] private Vector2 spawnForce = new Vector2();
    [SerializeField] private float curveDuration = 300f;
    [SerializeField] private AnimationCurve spawnDelayCurve;

    [SerializeField] private List<GameObject> meteorites = new List<GameObject>();

    private float curveTimer = 0f;
    private float spawnTimer = 0f;

    void Start()
    {

    }

    void Update()
    {
        curveTimer += Time.deltaTime * 100f;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelayCurve.Evaluate((curveTimer / curveDuration) / 100f))
        {
            spawnTimer = 0f;
            SpawnMeteorite();
        }
    }

    public void SpawnMeteorite()
    {
        Meteorite newMeteorite = Instantiate(meteorites[Random.Range(0, meteorites.Count)], GetRandomSpawnPoint(), Quaternion.identity).GetComponent<Meteorite>();
        newMeteorite.Gravitable.AddForce((GameManager.instance.transform.position - newMeteorite.transform.position).normalized * Random.Range(spawnForce.x, spawnForce.y));
    }

    public Vector3 GetRandomSpawnPoint()
    {
        return
            Random.Range(spawnDistance.x, spawnDistance.y)
            * new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0f, 0.8f),
            Random.Range(-1f, 1f)
            ).normalized;
    }
}
