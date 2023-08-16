using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal int CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO recipes (title, instructions, img, category, creatorId)
        VALUES (@TITLE, @Instructions, @Img, @Category, @CreatorId);
        SELECT LAST_INSERT_ID()
        ;";
        int recipeId = _db.ExecuteScalar<int>(sql, recipeData);
        return recipeId;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT * FROM recipes WHERE Id = @recipeId
        ;";
        Recipe recipe = _db.QueryFirstOrDefault<Recipe>(sql ,new {recipeId});
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        string sql = "SELECT * FROM recipes;";
        List<Recipe> recipes = _db.Query<Recipe>(sql).ToList();
        return recipes;
    }
}
