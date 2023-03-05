using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Lamya.CrossyRoad
{
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {

        Vector3 playerTargetPosition;
        [SerializeField] private float smoothing = 0.25f;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask waterMask;
        private bool animationPlaying;
        private int score;


        // Start is called before the first frame update
        void Start()
        {
            playerTargetPosition = transform.position;
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMovementDirection();
            MoveToTarget();
            if (playerTargetPosition.x <= -11 || playerTargetPosition.x >= 11)
            {
                GameOver.Instance.GameEnded();
            }
            
        }

        private void PlayerMovementDirection()
        {
            if (animationPlaying)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!CheckGrass())
                {
                    playerTargetPosition.z += 3;
                    PlayAnimation();
                    IncreaseScore(); 
                }
                CheckWater();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerTargetPosition.z-=3;
                PlayAnimation();
                DecreaseScore();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerTargetPosition.x+=3;
                PlayAnimation();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerTargetPosition.x-=3;
                PlayAnimation();
            }

        }

        void MoveToTarget()
        {
            Vector3 smoothFollow = Vector3.Lerp(transform.position, playerTargetPosition, smoothing);
            transform.position = smoothFollow;   
        }

        void PlayAnimation()
        {
            animator.SetTrigger("JumpTrigger");
            animationPlaying = true;
        }

        void AnimationEnded()
        {
            animationPlaying = false;
        }

        void IncreaseScore()
        {
            score++;
            scoreText.text = score.ToString();
        }

        void DecreaseScore()
        {
            if (score !=0)
            {
                score--;
                scoreText.text = score.ToString();
            }
        }

        void CheckWater()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag == "water")
                {
                    GameOver.Instance.GameEnded();
                }else if(hitInfo.collider.tag == "log")
                {
                    //have a moving animation for the player with the log + update the x for player target position and roundtoint
                }
            }
        }

        public bool CheckGrass()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 2.5f))
            {
                if (hitInfo.collider.tag == "grassObstacle")
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

    }

}

