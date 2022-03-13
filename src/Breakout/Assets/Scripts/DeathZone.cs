using UnityEngine;

namespace Assets.Scripts
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D colider)
        {
            if (colider.transform.CompareTag(Ball.BallTag) == false)
            {
                return;
            }

            Ball ball = colider.gameObject.GetComponent<Ball>();
            GameController.Instance.BallDied(ball);
        }
    }
}
