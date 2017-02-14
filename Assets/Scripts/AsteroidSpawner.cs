using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

    [Range(0f, 100f)]
    public float warpTunnelRadius = 50f;
    [Range(0f, 1f)]
    public float minimumAsteroidProbability;
    public int length = 30;
    public int asteroidsPerSector = 10;
    public int sectors = 8;
    public int jitterRange = 10;
    public int lengthBetweenDisks;
    
    public GameObject[] asteroids;
    public AnimationCurve distribution;
    public Material asteroidMaterial;
    public Mesh asteroidMesh;

    private ArrayList asteroidObjects;
    private GameObject asteroidParent;

    // Use this for initialization
    void Start() { 
        spawnAsteroids(asteroidsPerSector);
    }

    // Update is called once per frame
    void Update() {

    }

    void spawnAsteroids(int max) {
        asteroidParent = new GameObject("asteroidParent");
        asteroidObjects = new ArrayList();
        // Hand like a clock, it rotates
        Vector3 hand = new Vector3(0, 1, 0);
        Vector3 jitter;
        GameObject asteroidTemp, childTemp;
        for (int k = 1; k < sectors; k++)
        {
            for (int j = 1; j < length; j++)
            {
                for (int i = 1; i < max; ++i)
                {
                    jitter = Vector3.up * Random.Range(-jitterRange, jitterRange)
                        + Vector3.right * Random.Range(-jitterRange, jitterRange);
                    float coef = distribution.Evaluate(Random.value);
                    Debug.Log(coef);
                    if (coef > minimumAsteroidProbability) {
                        asteroidTemp = new GameObject("asteroid " + i);
                        asteroidTemp.transform.position = hand * coef * warpTunnelRadius + jitter;
                        asteroidTemp.transform.SetParent(asteroidParent.transform);
                        childTemp = (GameObject)GameObject.Instantiate(
                            asteroids[Random.Range(0, asteroids.Length)],
                            hand * coef * warpTunnelRadius + jitter,
                            Random.rotation);
                        childTemp.transform.SetParent(asteroidTemp.transform);
                        asteroidObjects.Add(asteroidTemp);
                    }
                }
                hand = Quaternion.AngleAxis(360 / sectors, Vector3.forward) * hand;
            }
            hand += Vector3.forward * lengthBetweenDisks;
        }
    }

    void FixedUpdate()
    {
        //foreach (GameObject g in asteroidObjects)
        //{
        //    Graphics.DrawMesh(asteroidMesh,
        //        g.transform.position,
        //        Random.rotation,
        //        asteroidMaterial,
        //        0);
        //}
    }
}
