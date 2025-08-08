using UnityEngine;
using StarterAssets;
public class Ring : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonController controller = other.GetComponent<ThirdPersonController>();
        if (controller != null && controller.IsGliding())
        {
            controller.TriggerGlideSpeedBoost(duration, speedMultiplier);
            Destroy(gameObject);
        }
    }
}
