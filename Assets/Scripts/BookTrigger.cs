using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    public GameObject recipeUI;

    private void OnMouseDown()
    {
        recipeUI.SetActive(true);
    }
}