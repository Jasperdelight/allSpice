using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _recipesRepository;

    public RecipesService(RecipesRepository recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {   
        int recipeId = _recipesRepository.CreateRecipe(recipeData);
        Recipe recipe = GetRecipeById(recipeId);
        return recipe;
    }
    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
        if(recipe == null)
        {
            throw new Exception("bad request");
        }
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _recipesRepository.GetRecipes();
        return recipes;
    }

    internal void RemoveRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId)
        {
            throw new Exception("You can't delete this");
        }
        _recipesRepository.RemoveRecipe(recipeId);
    }
}
