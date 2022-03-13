using UnityEngine;

namespace Assets.Scripts
{
    public enum BallState
    {
        Free,
        Caught
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        public static readonly string BallTag = "Ball";

        private BallState _ballState = BallState.Caught;
        private Rigidbody2D _rigidBody2D;

        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }

        public void Kick(Vector3 force)
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody2D.AddForce(force, ForceMode2D.Impulse);
        }

        public void Catch()
        {
            _rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody2D.gravityScale = 0f;
            _rigidBody2D.angularVelocity = 0f;
            _rigidBody2D.velocity = Vector2.zero;
            _ballState = BallState.Caught;
        }
    }
}
