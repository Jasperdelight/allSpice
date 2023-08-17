using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateIngredient(Ingredient iData)
    {
        string sql = @"
        INSERT INTO ingredients(name, quantity, recipeId)
        VALUES (@Name, @Quantity, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";
        int ingredientId = _db.ExecuteScalar<int>(sql, iData);
        return ingredientId;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
        SELECT * FROM ingredients WHERE id = @ingredientID
        ;";
        Ingredient ingredient = _db.QueryFirstOrDefault<Ingredient>(sql, new {ingredientId});
        return ingredient;
    }
}
