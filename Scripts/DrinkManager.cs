using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public static DrinkManager Instance;

    public GameObject drinkPrefab;
    public Transform spawnPoint;
    public RecipeBook recipeBook;

    public List<string> ingredients = new List<string>();
    public string currentDrinkName;

    [System.Serializable]
    public class DrinkSpritePair
    {
        public string drinkName;
        public Sprite sprite;
    }

    public List<DrinkSpritePair> drinkSprites;
    private Dictionary<string, Sprite> spriteMap;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        spriteMap = new Dictionary<string, Sprite>();
        foreach (var pair in drinkSprites)
        {
            if (!spriteMap.ContainsKey(pair.drinkName))
                spriteMap.Add(pair.drinkName, pair.sprite);
        }
    }

    public void AddIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void FinishDrink(List<string> ingredients)
    {
        currentDrinkName = recipeBook.GetDrinkName(ingredients);

        if (string.IsNullOrEmpty(currentDrinkName))
        {
            currentDrinkName = "Муть";
        }

        Session.Instance.finishedDrinkName = currentDrinkName;

        Sprite drinkSprite = null;
        if (spriteMap.TryGetValue(currentDrinkName, out Sprite foundSprite))
        {
            drinkSprite = foundSprite;
        }

        Session.Instance.finishedDrinkSprite = drinkSprite;

        GameObject newDrink = Instantiate(drinkPrefab, spawnPoint.position, Quaternion.identity);
        DrinkItem drinkItem = newDrink.GetComponent<DrinkItem>();
        drinkItem.drinkName = currentDrinkName;

        if (drinkSprite != null)
        {
            newDrink.GetComponent<SpriteRenderer>().sprite = drinkSprite;
        }

        currentDrinkName = "";
        ingredients.Clear();
    }
}