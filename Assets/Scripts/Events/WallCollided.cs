using TinyMessenger;
using UnityEngine;

namespace Events
{
    public class WallCollided : ITinyMessage
    {
        public GameObject Sender { get; set; }

        object ITinyMessage.Sender
        {
            get { return Sender; }
        }
    }
}