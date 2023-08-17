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

    internal Recipe EditRecipe(Recipe ogRecipe)
    {
        string sql = @"
        UPDATE recipes
        SET
        category = @Category,
        title = @Title,
        instructions = @Instructions
        WHERE id = @Id
        LIMIT 1;
        SELECT * FROM recipes WHERE id = @Id
        ;";
        Recipe updatedRecipe = _db.QueryFirstOrDefault<Recipe>(sql, ogRecipe);
        return updatedRecipe;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT
        rec.*,
        acc.*
        FROM recipes rec
        JOIN accounts acc ON acc.id = rec.creatorId
        WHERE rec.id = @recipeId LIMIT 1
        ;";
        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(
            sql ,
            (recipe, profile)=>
            {
                recipe.Creator = profile;
                return recipe;
            },
        new {recipeId}).FirstOrDefault();
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        string sql = @"
        SELECT 
        rec.*,
        acc.* 
        FROM recipes rec
        JOIN accounts acc ON acc.id = rec.creatorId
        ;";
        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(
        sql,
        (recipes, profile) =>
        {
            recipes.Creator = profile;
            return recipes;
        }
        ).ToList();
        return recipes;
    }

    internal void RemoveRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
        _db.Execute(sql, new{recipeId});
    }
}
