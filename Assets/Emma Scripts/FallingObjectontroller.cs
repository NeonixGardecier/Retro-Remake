using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    float wait = 0.1f;
    public GameObject FallingObjectPreFab;
    public Transform SpawnPoint;

    void Start()
    {
        SpawnFallingObjectPreFab();
    }

    void SpawnFallingObjectPreFab()
    {
        Instantiate(FallingObjectPreFab, SpawnPoint.position, Quaternion.identity);
    }
}

//chatgpt only fixed my syntax errors 