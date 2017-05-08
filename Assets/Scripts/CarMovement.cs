using System;
using System.Linq;
using UnityEditor;
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

        public int Points;


        // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("GoodPoint"))
            {
                Points++;
                Destroy(collision.gameObject);
            }


            if (collision.gameObject.CompareTag("BadPoint"))
            {
                Points--;
                Destroy(collision.gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            var leftWheelMultiply = (GetAntenaValue(RightAntena, GoodPoints) -
                                     0.5f * GetAntenaValue(RightAntena, BadPoints));
            var rightWheelMultiply = (GetAntenaValue(LeftAntena, GoodPoints) -
                                      0.5f * GetAntenaValue(LeftAntena, BadPoints));

            Debug.LogFormat("{0}   :    {1}", leftWheelMultiply, rightWheelMultiply);

            LeftWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * leftWheelMultiply;
            RightWheel.GetComponent<Rigidbody2D>().velocity = transform.up * Speed * rightWheelMultiply;
        }

        float GetAntenaValue(GameObject antena, GameObject pointsHolder)
        {
            var antenaPosition = antena.transform.position;

            var antenaValue = pointsHolder
                .transform.Cast<Transform>() //foreach
                .Sum(point => NormDistributionLike(point.position, antenaPosition));
            return antenaValue;
        }

        private static float GravityForceLike(Vector3 point, Vector3 antenaPosition)
        {
            return 1 / (point - antenaPosition).sqrMagnitude;
        }

        private static float NormDistributionLike(Vector3 point, Vector3 antenaPosition)
        {
            var distance = (point - antenaPosition).sqrMagnitude;
            const int pow = 8; // wyliczenie 2c^2   Math.Pow(2 * 2, 2);
            return (float)Math.Exp(-distance / pow);
        }
    }
}