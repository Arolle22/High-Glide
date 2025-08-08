using UnityEngine;

public class Beacon : MonoBehaviour
{
    [Header("Waypoint UI")]
    public RectTransform markerUI;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private Vector3 beaconPosition = new Vector3(3347.914f, 513.4f, 3137.868f);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (markerUI == null)
        {
            Debug.LogError("Marker UI not assigned!");
        }
        else
        {
            markerUI.gameObject.SetActive(true);
        }
    }

    private float smoothingSpeed = 10f;

    void Update()
    {
        if (markerUI == null || mainCamera == null)
            return;

        Vector3 worldPos = beaconPosition + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        float padding = 50f;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Vector2 targetPos;
        float targetAngle;

        if (screenPos.z > 0)
        {
            Vector2 beaconScreenPos = new Vector2(screenPos.x, screenPos.y);

            bool onScreenX = beaconScreenPos.x > padding && beaconScreenPos.x < Screen.width - padding;
            bool onScreenY = beaconScreenPos.y > padding && beaconScreenPos.y < Screen.height - padding;

            if (onScreenX && onScreenY)
            {
                Vector2 direction = (beaconScreenPos - screenCenter);
                float distance = direction.magnitude;

                float maxDistanceX = (Screen.width / 2f) - padding;
                float maxDistanceY = (Screen.height / 2f) - padding;
                float maxDistance = Mathf.Min(maxDistanceX, maxDistanceY);

                float clampedDistance = Mathf.Min(distance, maxDistance);
                float t = clampedDistance / maxDistance;
                Vector2 dirNormalized = direction.normalized;

                Vector2 clampedEdgePos = dirNormalized * maxDistance;
                targetPos = Vector2.Lerp(screenCenter + clampedEdgePos, screenCenter, 1 - t);

                Vector2 rotateDir = (targetPos - screenCenter).normalized;
                targetAngle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg - 90;
            }
            else
            {
                float clampedX = Mathf.Clamp(screenPos.x, padding, Screen.width - padding);
                float clampedY = Mathf.Clamp(screenPos.y, padding, Screen.height - padding);
                targetPos = new Vector2(clampedX, clampedY);

                Vector2 direction = (targetPos - screenCenter).normalized;
                targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            }
        }
        else
        {
            Vector3 flippedScreenPos = new Vector3(Screen.width - screenPos.x, Screen.height - screenPos.y, screenPos.z);

            float clampedX = Mathf.Clamp(flippedScreenPos.x, padding, Screen.width - padding);
            float clampedY = Mathf.Clamp(flippedScreenPos.y, padding, Screen.height - padding);

            Vector2 clampedPos = new Vector2(clampedX, clampedY);

            float distLeft = clampedX - padding;
            float distRight = (Screen.width - padding) - clampedX;
            float distBottom = clampedY - padding;
            float distTop = (Screen.height - padding) - clampedY;

            float minDist = Mathf.Min(distLeft, distRight, distTop, distBottom);

            if (minDist == distLeft) clampedPos.x = padding;
            else if (minDist == distRight) clampedPos.x = Screen.width - padding;
            else if (minDist == distBottom) clampedPos.y = padding;
            else if (minDist == distTop) clampedPos.y = Screen.height - padding;

            targetPos = clampedPos;

            Vector2 direction = (targetPos - screenCenter).normalized;
            targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }

        markerUI.position = Vector2.Lerp(markerUI.position, targetPos, smoothingSpeed * Time.deltaTime);

        Quaternion currentRot = markerUI.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, targetAngle);
        markerUI.rotation = Quaternion.Slerp(currentRot, targetRot, smoothingSpeed * Time.deltaTime);

        markerUI.gameObject.SetActive(true);
    }

        private void OnTriggerEnter(Collider other)
            {
                if (other.CompareTag("Player"))
                {
                    var playerController = other.GetComponent<StarterAssets.ThirdPersonController>();
                    if (playerController != null)
                    {
                        playerController.ReachBeacon();
                    }
                }
            }

}
