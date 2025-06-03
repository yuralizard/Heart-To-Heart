using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadKitchen()
    {
        SceneManager.LoadScene("KitchenScene");
    }

    public void LoadHall()
    {
        SceneManager.LoadScene("HallScene");
    }
}
