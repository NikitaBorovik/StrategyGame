using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace App.World.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private float maximumZoom;
        [SerializeField]
        private float minimumZoom;
        [SerializeField]
        private float zoomSpeed = 10f;
        [SerializeField]
        private Transform cameraTarget;
        [SerializeField]
        private float cameraMovementSpeed;
        [SerializeField]
        private int cameraMovementZoneWidth;
        [SerializeField]
        private PolygonCollider2D cameraConfiner;
        private float zoomDistance;

        private void Awake()
        {
            Camera.main.eventMask = LayerMask.GetMask("UI");
        }

        public int CameraMovementZoneWidth { get => cameraMovementZoneWidth; }

        public void ChangeZoom(float axisValue)
        {
            zoomDistance = axisValue * zoomSpeed;
            virtualCamera.m_Lens.OrthographicSize -= zoomDistance;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, minimumZoom, maximumZoom);
        }
        public void MoveCamera(Direction direction)
        {
            float xMove = 0f;
            float yMove = 0f;
            switch (direction)
            {
                case Direction.Left:
                    xMove = -cameraMovementSpeed * Time.deltaTime;
                    break;

                case Direction.Right:
                    xMove = cameraMovementSpeed * Time.deltaTime;
                    break;

                case Direction.Up:
                    yMove = -cameraMovementSpeed * Time.deltaTime;
                    break;

                case Direction.Down:
                    yMove = cameraMovementSpeed * Time.deltaTime;
                    break;
            }
            float xPos = cameraTarget.position.x + xMove;
            float yPos = cameraTarget.position.y + yMove;
            if (cameraConfiner != null)
            {
                float camHeigth = 2 * virtualCamera.m_Lens.OrthographicSize;
                float camWidth = camHeigth * virtualCamera.m_Lens.Aspect; 
                xPos = Mathf.Clamp(xPos, cameraConfiner.bounds.min.x + camWidth / 2, cameraConfiner.bounds.max.x - camWidth / 2);
                yPos = Mathf.Clamp(yPos, cameraConfiner.bounds.min.y + camHeigth / 2, cameraConfiner.bounds.max.y - camHeigth / 2);
            }
            cameraTarget.position = new Vector3(xPos, yPos);
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }

}

