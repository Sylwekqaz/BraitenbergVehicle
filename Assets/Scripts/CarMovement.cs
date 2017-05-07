using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarMovement : MonoBehaviour
    {
        public int Speed;

        public GameObject LeftWheel;
        public GameObject RightWheel;
        public GameObject LeftAntena;
        public GameObject RightAntena;

        public GameObject BadPoints;
        public GameObject GoodPoints;


        // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.tag);
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            LeftWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed* GetAntenaValue(RightAntena , GoodPoints);
            RightWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * GetAntenaValue(LeftAntena, GoodPoints);
        }

        float GetAntenaValue(GameObject antena, GameObject pointsHolder)
        {
            var antenaPosition = antena.transform.position;

            return pointsHolder
                .transform.Cast<Transform>()
                .Sum(point => 1 / (point.position - antenaPosition).sqrMagnitude);
        }
    }
}