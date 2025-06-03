using UnityEngine;

public class DrinkItem : MonoBehaviour
{
    public string drinkName;
    private bool isHeld = false;
    private bool canBeDelivered = false;

    public bool IsHeld() => isHeld;
    public bool CanBeDelivered() => canBeDelivered;

    private void Start()
    {
        if (string.IsNullOrEmpty(Session.Instance.finishedDrinkName))
        {
            Destroy(gameObject);
            return;
        }

        drinkName = Session.Instance.finishedDrinkName;
        GetComponent<SpriteRenderer>().sprite = Session.Instance.finishedDrinkSprite;
    }

    private void Update()
    {
        if (isHeld)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                isHeld = false;
                Invoke(nameof(EnableDelivery), 0.1f);
            }
        }
    }

    private void OnMouseDown()
    {
        isHeld = true;
        canBeDelivered = false;
    }

    public void EnableDelivery()
    {
        canBeDelivered = true;
    }
}