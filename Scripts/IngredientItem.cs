using UnityEngine;

public class IngredientItem : MonoBehaviour
{
    public string ingredientName;
    public IngredientBox originBox;

    private bool isHeld = false;

    private void Update()
    {
        if (isHeld)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;

            if (Input.GetMouseButtonUp(0))
                isHeld = false;
        }

        Physics2D.SyncTransforms();
    }

    public void OnMouseDown()
    {
        isHeld = true;
    }

    private void OnDestroy()
    {
        if (originBox != null)
        {
            originBox.ClearHandful();
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}