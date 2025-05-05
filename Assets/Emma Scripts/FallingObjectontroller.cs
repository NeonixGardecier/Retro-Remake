using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    float wait = 1f;
    private GameObject _instance;
    public GameObject FallingObjectPreFab;
    public Transform SpawnPoint;

    void Start()
    {
        Invoke(nameof(SpawnFallingObjectPreFab),wait);
    
    }
    
    void SpawnFallingObjectPreFab()
    {
        if (_instance) return;

        Vector3 spawnPos = SpawnPoint != null ? SpawnPoint.position : transform.position;

        _instance = Instantiate(FallingObjectPreFab,spawnPos,Quaternion.identity);
    }

    

}

