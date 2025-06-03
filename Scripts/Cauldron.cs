using UnityEngine;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour
{
    public List<string> currentIngredients = new List<string>();
    public RecipeBook recipeBook;
    public DrinkManager drinkManager;

    public SpriteRenderer cauldronRenderer;
    public Sprite emptySprite;
    public Sprite waterSprite;
    public Sprite alcoholSprite;

    private void Start()
    {
        if (cauldronRenderer != null)
            cauldronRenderer.sprite = emptySprite;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        BottleItem bottle = other.GetComponent<BottleItem>();
        if (bottle != null)
        {
            if (!bottle.IsHeld())
            {
                currentIngredients.Add(bottle.ingredientName);
                bottle.ReturnToStart();

                UpdateLiquidVisual(bottle.ingredientName);
            }
            return;
        }

        IngredientItem item = other.GetComponent<IngredientItem>();
        if (item != null)
        {
            if (!item.IsHeld())
            {
                currentIngredients.Add(item.ingredientName);
                Destroy(other.gameObject);
            }
            return;
        }
    }

    private void OnMouseDown()
    {
        drinkManager.FinishDrink(currentIngredients);
        currentIngredients.Clear();
        ResetVisual();
    }

    private void UpdateLiquidVisual(string ingredientName)
    {
        if (cauldronRenderer == null) return;

        if (ingredientName.ToLower().Contains("water"))
        {
            cauldronRenderer.sprite = waterSprite;
        }
        else if (ingredientName.ToLower().Contains("alcohol"))
        {
            cauldronRenderer.sprite = alcoholSprite;
        }
    }

    private void ResetVisual()
    {
        if (cauldronRenderer != null)
            cauldronRenderer.sprite = emptySprite;
    }
}
