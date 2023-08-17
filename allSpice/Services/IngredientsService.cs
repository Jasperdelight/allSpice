using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _ingredientsRepository;

    public IngredientsService(IngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    internal Ingredient CreateIngredient(Ingredient iData)
    {
        
        int ingredientId = _ingredientsRepository.CreateIngredient(iData);
        Ingredient ingredient = GetIngredientById(ingredientId);
        return ingredient;
    }
    internal Ingredient GetIngredientById(int ingredientId)
    {
      Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
      if (ingredient == null)
      {
        throw new Exception("Bad Id");
      }
      return ingredient;
    }
}
