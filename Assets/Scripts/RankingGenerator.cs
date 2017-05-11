using UnityEngine;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class RankingGenerator : MonoBehaviour
{

    public GameObject CarHolder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        var builder = new StringBuilder();
	    builder.AppendLine("Ranking:");

	    CarHolder.transform
            .Cast<Transform>()
            .Select(t => t.gameObject.GetComponent<CarMovement>())
            .OrderByDescending(m => m.BatteryLevel)
            .ToList()
            .ForEach(m => builder.AppendFormat("Car: {0:P0} \n", m.BatteryLevel));

        GetComponent<Text>().text = builder.ToString();
	}
}
