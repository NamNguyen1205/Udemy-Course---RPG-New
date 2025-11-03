using UnityEngine;

public class Entity_SFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX Name")]
    [SerializeField] private string attackHit;
    [SerializeField] private string attackMiss;
    [Space]
    [SerializeField] private float soundDistance = 15f;
    [SerializeField] private bool showGizmos;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlayAttackHit()
    {
        AudioManager.instance.PlaySFX(attackHit, audioSource, soundDistance);
    }

    public void PlayAttackMiss()
    {
        AudioManager.instance.PlaySFX(attackMiss, audioSource, soundDistance);
    }

    void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;
            
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, soundDistance);
    }
}
