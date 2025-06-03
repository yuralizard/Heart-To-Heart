using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string drinkName;
    public List<string> ingredients;
}

public class RecipeBook : MonoBehaviour
{
    public List<Recipe> recipes;

    public string GetDrinkName(List<string> currentIngredients)
    {
        foreach (Recipe recipe in recipes)
        {
            if (IsMatch(recipe.ingredients, currentIngredients))
            {
                return recipe.drinkName;
            }
        }
        return null;
    }

    private bool IsMatch(List<string> recipeIngredients, List<string> currentIngredients)
    {
        if (recipeIngredients.Count != currentIngredients.Count)
            return false;

        for (int i = 0; i < recipeIngredients.Count; i++)
        {
            if (recipeIngredients[i] != currentIngredients[i])
                return false;
        }

        return true;
    }
}