using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    [System.Serializable]
    public class DrinkOrder
    {
        public string drinkName;
        public Sprite orderFace;
        public Sprite likeFace;
        public Sprite dislikeFace;
    }

    public DrinkOrder[] possibleOrders;
    private GameObject currentCustomer;

    void Start()
    {
        if (Session.Instance == null)
        {
            return;
        }

        if (!Session.Instance.orderCompleted && Session.Instance.customerSpawned)
        {
            RestoreCustomer();
            return;
        }

        SpawnCustomer();
    }

    private void Update()
    {
        if (readyForNextCustomer && currentCustomer == null)
        {
            readyForNextCustomer = false;
            Invoke(nameof(SpawnCustomer), 4f);
        }
    }

    public void SpawnCustomer()
    {
        if (currentCustomer != null || Session.Instance.customerSpawned)
        {
            return;
        }

        int index = Random.Range(0, possibleOrders.Length);
        DrinkOrder order = possibleOrders[index];

        Session.Instance.currentOrderName = order.drinkName;
        Session.Instance.orderCompleted = false;
        Session.Instance.customerSpawned = true;

        currentCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerItem customerItem = currentCustomer.GetComponent<CustomerItem>();

        customerItem.orderName = order.drinkName;
        customerItem.orderChar = order.orderFace;
        customerItem.likeChar = order.likeFace;
        customerItem.dislikeChar = order.dislikeFace;

        if (customerItem.orderTextUI != null)
        {
            customerItem.orderTextUI.text = order.drinkName;
        }
    }

    public void RestoreCustomer()
    {
        if (currentCustomer != null) return;

        string orderName = Session.Instance.currentOrderName;

        DrinkOrder order = null;

        foreach (var o in possibleOrders)
        {
            if (o.drinkName == orderName)
            {
                order = o;
                break;
            }
        }

        currentCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerItem customerItem = currentCustomer.GetComponent<CustomerItem>();

        customerItem.orderName = order.drinkName;
        customerItem.orderChar = order.orderFace;
        customerItem.likeChar = order.likeFace;
        customerItem.dislikeChar = order.dislikeFace;

        if (customerItem.orderTextUI != null)
        {
            customerItem.orderTextUI.text = order.drinkName;
        }
    }

    private bool readyForNextCustomer = false;
    public void MarkReadyForNextCustomer()
    {
        readyForNextCustomer = true;
    }

    public void ClearCustomer()
    {
        currentCustomer = null;
    }
}