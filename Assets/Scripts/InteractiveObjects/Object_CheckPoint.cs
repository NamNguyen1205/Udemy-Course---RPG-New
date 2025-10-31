using UnityEngine;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    private Object_CheckPoint[] allCheckPoints;
    private Animator anim;

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        allCheckPoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }

    public void ActivateCheckpoint(bool activate)
    {
        anim.SetBool("isActive", activate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var point in allCheckPoints)
            point.ActivateCheckpoint(false);

        // save position to save manager
        SaveManager.instance.GetGameData().savedCheckpoint = (Vector2)transform.position;
        ActivateCheckpoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.savedCheckpoint == transform.position;
        ActivateCheckpoint(active);

        if(active)
            Player.instance.TeleportPlayer(transform.position);
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
