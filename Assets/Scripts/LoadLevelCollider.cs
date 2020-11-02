using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelCollider:MonoBehaviour
{
    public int level;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(level);
        }
    }
}