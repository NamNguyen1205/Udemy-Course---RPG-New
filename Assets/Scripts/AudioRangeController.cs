using UnityEngine;

public class AudioRangeController : MonoBehaviour
{
    private AudioSource source;
    private Transform player;

    private float minDistanceToHearSound = 12f;
    private float maxVolume = 1;
    [SerializeField] private bool showGizmos;

    void Start()
    {
        player = Player.instance.transform;
        source = GetComponent<AudioSource>();


    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(player.position, transform.position);
        float t = Mathf.Clamp01(1 - (distance / minDistanceToHearSound));

        float targetVolume = Mathf.Lerp(0, maxVolume, t * t);
        source.volume = Mathf.Lerp(source.volume, targetVolume, Time.deltaTime * 3);
    }

    void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;
            
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minDistanceToHearSound);
    }
}
