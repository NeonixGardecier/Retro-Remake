using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public int level;
    
    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }
    }
}
