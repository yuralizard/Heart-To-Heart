using UnityEngine;
using TMPro;

public class CustomerItem : MonoBehaviour
{
    public string orderName;
    public Sprite orderChar;
    public Sprite likeChar;
    public Sprite dislikeChar;

    private SpriteRenderer spriteRenderer;
    private bool hasReacted = false;
    public TextMeshProUGUI orderTextUI;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = orderChar;

        orderName = Session.Instance.currentOrderName;

        if (orderTextUI != null)
        {
            orderTextUI.text = orderName;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hasReacted) return;

        DrinkItem drink = other.GetComponent<DrinkItem>();
        if (drink != null && drink.CanBeDelivered())
        {
            bool success = drink.drinkName == orderName;
            React(success);
            Destroy(drink.gameObject);
            hasReacted = true;
        }
    }

    public void React(bool success)
    {
        spriteRenderer.sprite = success ? likeChar : dislikeChar;

        if (orderTextUI != null)
            Destroy(orderTextUI.gameObject);

        Session.Instance.finishedDrinkName = "";
        Session.Instance.CompleteOrder();

        if (success)
            GameManager.Instance.AddScore(1);
        else
            GameManager.Instance.AddScore(-1);

        FindFirstObjectByType<CustomerSpawner>().ClearCustomer();
        CustomerSpawner spawner = FindAnyObjectByType<CustomerSpawner>();
        if (spawner != null)
        {
            spawner.MarkReadyForNextCustomer();
        }
        Destroy(gameObject, 1f);
    }
}