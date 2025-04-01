using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ShopManager").AddComponent<ShopManager>();
            }
            return instance;
        }
    }

    public ItemShop itemShop;
    public ItemShop ItemShop
    {
        get { return itemShop; }
        set { itemShop = value; }
    }

    public EmployShop employShop;
    public EmployShop EmployShop
    {
        get { return employShop; }
        set { employShop = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
