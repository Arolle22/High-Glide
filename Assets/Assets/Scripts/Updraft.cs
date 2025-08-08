using System.Collections;
using UnityEngine;

public class Updraft : MonoBehaviour
{
    [Tooltip("How much upward force to apply when gliding through")]
    public float updraftStrength = 10f;

    [Tooltip("How long to wait before destroying this updraft after contact")]
    public float destroyDelay = 3f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<StarterAssets.ThirdPersonController>();
        if (controller != null && controller.IsGliding() && !triggered)
        {
            controller.ApplyUpdraft(updraftStrength);
            triggered = true;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
