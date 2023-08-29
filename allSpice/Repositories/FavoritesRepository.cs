using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite fData)
    {
        string sql = @"
        INSERT INTO favorites(id, accountId, recipeId)
        VALUES (@Id, @AccountId, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";
        int favoriteId = _db.ExecuteScalar<int>(sql, fData);
        fData.Id = favoriteId;
        return fData;
    }

    internal List<Favorite> GetAccountFavorites(string accountId)
    {
        string sql = @"
        SELECT 
        acc.*,
        rec.*,
        fav.*
        FROM favorites fav
        JOIN accounts acc on acc.id = fav.accountId
        JOIN recipes rec ON fav.recipeId = rec.id
        WHERE acc.id = @accountId
        ;";
        List<Favorite> favorites = _db.Query<Account, Recipe, Favorite, Favorite>(
            sql,
            (profile, recipe, favorite) =>
            {
                favorite.Creator = profile;
                favorite.Recipe = recipe;
                return favorite;
            },
            
             new {accountId}).ToList();
        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = @"
        SELECT 
        acc.*,
        rec.*,
        fav.*
        FROM favorites fav
        JOIN accounts acc on acc.id = fav.accountId
        JOIN recipes rec ON fav.recipeId = rec.id
        WHERE fav.id = @favoriteId
        ;";
        Favorite favorite = _db.Query<Account, Recipe, Favorite, Favorite>
        
        (sql,
        (account, recipe, favorite) =>
        {
            favorite.Creator = account;
            favorite.Recipe = recipe;
            return favorite;
        },
        
         new {favoriteId}).FirstOrDefault();
        return favorite;
    }

    internal void RemoveFavorite(string userId, int favoriteId)
    {
        string sql = @"DELETE FROM favorites WHERE id = @favoriteId LIMIT 1
        ;";
        _db.Execute(sql, new{favoriteId});
    }
}
