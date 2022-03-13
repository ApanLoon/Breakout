using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public enum PaddleState
    {
        Free,
        BallCatcher,
        BallCaught
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 15f;
        [SerializeField] private float _kickForce = 10f;
        [SerializeField] private float _velocityBleedFactor = 0.1f;

        private Rigidbody2D _rigidbody2D;

        private PaddleState _state = PaddleState.Free;
        private Ball _caughtBall;
        private Vector2 _ballOffset;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _state = PaddleState.BallCatcher;
        }

        private void Update()
        {
            if (_state == PaddleState.BallCaught)
            {
                _caughtBall.transform.position = (Vector2)transform.position + _ballOffset;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Wall") == true)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            if (collision.transform.CompareTag(Ball.BallTag) == false)
            {
                return;
            }

            Ball ball = collision.gameObject.GetComponent<Ball>();

            switch (_state)
            {
                case PaddleState.BallCatcher:
                {
                    _caughtBall = ball;
                    _caughtBall.Catch();
                    _ballOffset = collision.contacts[0].point - (Vector2)transform.position;
                    _state = PaddleState.BallCaught;
                    break;
                }
                case PaddleState.BallCaught:
                case PaddleState.Free:
                    ball.Kick(_rigidbody2D.velocity * _velocityBleedFactor);// + new Vector2(0f, _kickForce));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPaddleMove(InputValue inputValue)
        {
            Vector2 input = inputValue.Get<Vector2>();
            _rigidbody2D.velocity = input * _moveSpeed;
        }


        public void OnPaddleKick()
        {
            if (_state == PaddleState.BallCaught)
            {
                _caughtBall.Kick(_rigidbody2D.velocity + new Vector2(0f, _kickForce));
                _caughtBall = null;
                _state = PaddleState.Free;
            }
        }
    }
}
