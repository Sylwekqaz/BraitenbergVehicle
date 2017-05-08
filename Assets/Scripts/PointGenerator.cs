using UnityEngine;
using System.Collections;

public class PointGenerator : MonoBehaviour
{

    public GameObject GoodPointsHolder;
    public GameObject BadPointsHolder;

    public GameObject GoodPoint;
    public GameObject BadPoint;

    public int BadPointCount;
    public int GoodPointCount;

    public int ArenaSize;

    // Use this for initialization
    void Start()
    {
        var arenaHalfSize = ArenaSize / 2;
        for (int i = 0; i < GoodPointCount; i++)
        {
            Instantiate(GoodPoint, new Vector3(Random.Range(-arenaHalfSize, arenaHalfSize), Random.Range(-arenaHalfSize, arenaHalfSize)), Quaternion.identity, GoodPointsHolder.transform);
        }

        for (int i = 0; i < BadPointCount; i++)
        {
            Instantiate(BadPoint, new Vector3(Random.Range(-arenaHalfSize, arenaHalfSize), Random.Range(-arenaHalfSize, arenaHalfSize)), Quaternion.identity, BadPointsHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}