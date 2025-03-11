using UnityEngine;

namespace CookedOutPuzzle.PuzzlePieceController
{
    public class PuzzlePieceController : MonoBehaviour
    {
        public float moveSpeed = 10f; // Speed of movement
        public float lerpSpeed = 10f; // Smoothing factor
        public LayerMask stopLayer; // Assign this in Unity (for obstacles & puzzles)
        public LayerMask boardLayer; // Assign this to the board (for valid movement)

        private Vector3 targetPosition;
        private bool isDragging = false;

        void Start()
        {
            targetPosition = transform.position; // Initialize target position
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchWorldPos = GetTouchWorldPosition(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    if (IsTouchingThisObject(touchWorldPos))
                    {
                        isDragging = true;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    if (touchWorldPos != Vector3.zero && !IsObstacleInPath(touchWorldPos))
                    {
                        targetPosition = touchWorldPos;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }

            // Smoothly move to target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }

        Vector3 GetTouchWorldPosition(Vector2 touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, boardLayer))
            {
                return new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            return Vector3.zero;
        }

        bool IsObstacleInPath(Vector3 newPosition)
        {
            RaycastHit hit;
            Vector3 direction = newPosition - transform.position;
            float distance = direction.magnitude;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, distance, stopLayer))
            {
                return true; // Found an obstacle or puzzle piece
            }
            return false;
        }

        bool IsTouchingThisObject(Vector3 worldPosition)
        {
            Collider col = GetComponent<Collider>();
            return col.bounds.Contains(worldPosition);
        }
    }
}