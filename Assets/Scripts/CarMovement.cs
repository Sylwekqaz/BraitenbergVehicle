using UnityEngine;

namespace Assets.Scripts
{
    public class CarMovement : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D { get; set; }
        public int Speed;

        // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.tag);
        }

        // Use this for initialization
        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Rigidbody2D.velocity = Vector2.up * Speed;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}