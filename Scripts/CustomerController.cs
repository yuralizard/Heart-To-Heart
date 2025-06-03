using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public GameObject customer;

    public void ShowCustomer()
    {
        customer.SetActive(true);
    }
    public void HideCustomer()
    {
        customer.SetActive(false);
    }
    
}
