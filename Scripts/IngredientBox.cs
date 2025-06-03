using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public string ingredientName;
    public GameObject handfulPrefab;
    private GameObject currentHandful;

    public Transform spawnPoint;

    private void OnMouseDown()
    {
        if (currentHandful != null)
        {
            return;
        }

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0f;
        currentHandful = Instantiate(handfulPrefab, spawnPosition, Quaternion.identity);

        IngredientItem item = currentHandful.GetComponent<IngredientItem>();
        item.ingredientName = ingredientName;
        item.originBox = this;

        item.OnMouseDown();
    }

    public void ClearHandful()
    {
        currentHandful = null;
    }
}
