using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Lamya.CrossyRoad
{
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {

        private Vector3 playerTargetPosition;
        private bool animationPlaying;
        private bool onLog = false;
        private float speed;
        private float direction;
        private int score;
        [SerializeField] private float smoothing = 0.25f;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Animator animator;
        

        void Start()
        {
            playerTargetPosition = transform.position;
            score = 0;
        }

        void Update()
        {
            PlayerMovementDirection();
            MoveToTarget();
            CheckPlayerPos();
            PlayerLogMovement();
        }

        /// <summary>
        /// This method takes the input from the user and updates the player's target position accordingly after checking for the obstacles.
        /// </summary>
        private void PlayerMovementDirection()
        {
            if (animationPlaying)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
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
                transform.eulerAngles = new Vector3(0, 180, 0);

                if (!CheckGrass())
                {
                    playerTargetPosition.z -= 3;
                    PlayAnimation();
                    DecreaseScore();
                }
                CheckWater();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                if (!CheckGrass())
                {
                    playerTargetPosition.x += 3;
                    PlayAnimation();
                }
                CheckWater();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                if (!CheckGrass())
                {
                    playerTargetPosition.x -= 3;
                    PlayAnimation();
                }
                CheckWater();
            }

        }

        /// <summary>
        /// This method moves the player to their target position, it's called after the PlayerMovementDirection method in the update.
        /// </summary>
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

        /// <summary>
        /// This method is called in the event at the end of the animation
        /// </summary>
        void AnimationEnded()
        {
            animationPlaying = false;
        }

        /// <summary>
        /// Method that increases the score when the player moves forward and updates the text in the canvas.
        /// </summary>
        void IncreaseScore()
        {
            score++;
            scoreText.text = score.ToString();
        }

        /// <summary>
        /// Method that decreases the score when the player moves backward and updates the text in the canvas.
        /// </summary>
        void DecreaseScore()
        {
            if (score !=0)
            {
                score--;
                scoreText.text = score.ToString();
            }
        }

        /// <summary>
        /// This method is called in the update and checks the player's position.
        /// If the player moves too close to the edges of the terrains the player dies and the game ends.
        /// </summary>
        void CheckPlayerPos()
        {
            if (playerTargetPosition.x <= -25 || playerTargetPosition.x >= 25)
            {
                GameOver.Instance.GameEnded();
            }
        }

        /// <summary>
        /// This method casts a raycast infront of the player to check if it's on water or a log. It gets called if a player moves.
        /// If the collider is tagged water the player dies and the game ends, if it's tagged log then the onLog variable is set to true.
        /// </summary>
        void CheckWater()
        {
            if (Physics.Raycast(playerTargetPosition, Vector3.down, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag.Equals("water"))
                {
                    GameOver.Instance.GameEnded();

                }
                else if (hitInfo.collider.tag.Equals("log"))
                {
                    speed = hitInfo.collider.GetComponent<LogBehaviour>().Speed;
                    direction = hitInfo.collider.GetComponent<LogBehaviour>().Direction;
                    onLog = true;
                } 
            }
            else
            {
                onLog = false;
            }
        }

        /// <summary>
        /// This method is called in the update, but it's only accessed if the "onLog" variable is true.
        /// It's used to move the player with the log.
        /// </summary>
        void PlayerLogMovement()
        {
            if (onLog)
            {
                playerTargetPosition.x += direction * speed * Time.deltaTime;

            }
            else
            {
                playerTargetPosition.x = Mathf.RoundToInt(transform.position.x);
            }
           
        }

        /// <summary>
        /// This method casts a raycast infront of the player to check if a grass obstacle ("cube") is infront of them.
        /// it returns a bool value, if true then the player can't move towards that spot
        /// </summary>
        public bool CheckGrass()
        {
            if (Physics.Raycast(playerTargetPosition, transform.forward, out RaycastHit hitInfo, 2f))
            {
                if (hitInfo.collider.tag == "grassObstacle")
                {
                    return true;
                }
            }
             return false;
        }

    }

}

