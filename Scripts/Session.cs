using UnityEngine;

public class Session : MonoBehaviour
{
    public static Session Instance;

    public string currentOrderName;
    public bool orderCompleted = false;
    public bool customerSpawned = false;

    public string finishedDrinkName;
    public Sprite finishedDrinkSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteOrder()
    {
        orderCompleted = true;
        customerSpawned = false;
    }
}