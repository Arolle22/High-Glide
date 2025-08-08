using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("LaunchPad: player left pad, starting timer");
            TimerManager.Instance?.StartTimer();
        }
    }
}
