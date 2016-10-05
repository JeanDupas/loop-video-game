using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public Transform mapBorderLeft;
        public Transform mapBorderRight;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float yMargin = 4f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        private Vector3 m_targetPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            if (Mathf.Abs(transform.position.y - target.position.y) > yMargin)
                m_targetPos = target.position;
            else
                m_targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);

            Vector3 aheadTargetPos;
            //if ((transform.position.x > 10f) && (xMoveDelta>=0))
            //    aheadTargetPos = transform.position;
            //else
            aheadTargetPos = m_targetPos + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            var halfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
            var bottomLeft = newPos.x - halfWidth;
            var bottomRight = newPos.x + halfWidth;

            if (bottomLeft < mapBorderLeft.position.x)
            {
                newPos.x = mapBorderLeft.position.x + halfWidth; damping = 0;
            }
            else if (bottomLeft > mapBorderLeft.position.x)
            {
                damping = 1;
            }
            var mapRightCorner = mapBorderRight.position.x + mapBorderRight.GetComponent<Renderer>().bounds.size.x;
            if (bottomRight > mapRightCorner)
            {
                newPos.x = mapRightCorner - halfWidth; damping = 0;
            }
            else if (bottomRight < mapRightCorner)
            {
                damping = 1;
            }

            // Permet d'avoir la taille d'un demi écran de caméra
            /*
            if (newPos.x >= 20)
            { newPos.x = 20; damping = 0; }
            else if (newPos.x <= -56)
            { newPos.x = -56; ; damping = 0; }
            if (target.position.x<23 && target.position.x > 19)
                damping = 1;
            if (target.position.x > -66 && target.position.x < -55)
                damping = 1;
            */
            transform.position = newPos;
            
            m_LastTargetPosition = target.position;
        }
    }
}
