using UnityEngine;


namespace Lamya.CrossyRoad
{

    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] float smoothSpeed;
        private Transform myTransform;
        private Vector3 offset;
        private float previousZ;

        private void Awake()
        {
            myTransform = transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            offset = myTransform.position - playerTransform.position;
            previousZ = myTransform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            CameraMovementFollow();
        }


        void CameraMovementFollow()
        {
            Vector3 targetPos = playerTransform.position + offset;
            targetPos = new Vector3(targetPos.x, myTransform.position.y, targetPos.z < previousZ ? previousZ : targetPos.z);
            Vector3 smoothFollow = Vector3.Lerp(myTransform.position, targetPos, smoothSpeed);
            myTransform.position = smoothFollow;
            previousZ = myTransform.position.z;
        }
    }

}
