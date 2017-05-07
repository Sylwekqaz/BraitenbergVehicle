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
    // Use this for initialization
    void Start()
    {
        Instantiate(GoodPoint, new Vector3(0, 40), Quaternion.identity, GoodPointsHolder.transform);

        for (int i = 0; i < GoodPointCount; i++)
        {
            Instantiate(GoodPoint, new Vector3(Random.Range(-50,50), Random.Range(-50, 50)), Quaternion.identity, GoodPointsHolder.transform);
        }

        for (int i = 0; i < BadPointCount; i++)
        {
            Instantiate(BadPoint, new Vector3(Random.Range(-50,50), Random.Range(-50, 50)), Quaternion.identity, BadPointsHolder.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}