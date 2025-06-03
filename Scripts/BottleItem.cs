using UnityEngine;

public class BottleItem : MonoBehaviour
{
    public string ingredientName;

    private bool isHeld = false;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isHeld)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }

        if (isHeld && Input.GetMouseButtonUp(0))
        {
            isHeld = false;
        }
    }

    private void OnMouseDown()
    {
        isHeld = true;
    }

    public void ReturnToStart()
    {
        transform.position = startPosition;
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}