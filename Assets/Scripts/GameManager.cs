using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 lastDeathPostion;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetLastDeathPosition(Vector3 position) => lastDeathPostion = position;

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        string sceneName = SceneManager.GetActiveScene().name;
        ChangeScene(sceneName, RespawnType.None);
    }

    public void ChangeScene(string sceneName, RespawnType respawnType)
    {
        StartCoroutine(ChangeSceneCo(sceneName, respawnType));
    }

    private IEnumerator ChangeSceneCo(string sceneName, RespawnType respawnType)
    {
        //fade effect

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(0.2f);

        Vector3 position = GetNewPlayerPosition(respawnType);

        if (position != Vector3.zero)
            Player.instance.TeleportPlayer(position);

    }

    private Vector3 GetNewPlayerPosition(RespawnType type)
    {
        if (type == RespawnType.None)
        {
            var data = SaveManager.instance.GetGameData();
            var checkpoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
            var unlockedCheckpoints = checkpoints
                .Where(cp => data.unlockedCheckPoints.TryGetValue(cp.GetCheckpointId(), out bool unlocked) && unlocked)
                .Select(cp => cp.GetPosition())
                .ToList();

            var enterWaypoints = FindObjectsByType<Object_Waypoint>(FindObjectsSortMode.None)
            .Where(wp => wp.GetWaypointType() == RespawnType.Enter)
            .Select(wp => wp.GetPositionAndSetTriggerFalse())
            .ToList();

            var selectedPositions = unlockedCheckpoints.Concat(enterWaypoints).ToList(); // combine 2 list in to 1

            if (selectedPositions.Count == 0)
                return Vector3.zero;

            return selectedPositions.OrderBy(position => Vector3.Distance(position, lastDeathPostion)).First();
        }

        return GetWaypointPosition(type);
    }
    
    private Vector3 GetWaypointPosition(RespawnType type)
    {
        var waypoints = FindObjectsByType<Object_Waypoint>(FindObjectsSortMode.None);

        foreach (var point in waypoints)
        {
            if (point.GetWaypointType() == type)
                return point.GetPositionAndSetTriggerFalse();
    
        }

        return Vector3.zero;
    }
}
