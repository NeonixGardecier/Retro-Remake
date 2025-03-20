using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    float wait = 0.1f;
    public GameObject FallingObject;

    void Start()
    {
        InvokeRepeating("Fall", wait, wait);
    }

    void Fall()
    {
        Instantiate(FallingObject, new Vector3(Random.Range(-10, 10), 10, 0), Quaternion.identity);
    }
}

//chatgpt only fixed my syntax errors 