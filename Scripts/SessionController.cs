using UnityEngine;

public class SessionController : MonoBehaviour
{
    public GameObject sessionPrefab;

    void Awake()
    {
        if (Session.Instance == null)
        {
            GameObject obj = Instantiate(sessionPrefab);
            obj.name = "Session";
        }
    }
}