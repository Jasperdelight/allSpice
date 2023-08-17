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

    internal int CreateFavorite(Favorite fData)
    {
        string sql = @"
        INSERT INTO favorites(id, accountId, recipeId)
        VALUES (@Id, @AccountId, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";
        int favoriteId = _db.ExecuteScalar<int>(sql, fData);
        return favoriteId;
    }

    internal List<Favorite> GetAccountFavorites(string accountId)
    {
        string sql = @"
        SELECT * FROM favorites WHERE accountId = @accountId
        ;";
        List<Favorite> favorites = _db.Query<Favorite>(sql, new {accountId}).ToList();
        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = @"
        SELECT * FROM favorites WHERE id = @favoriteId
        ;";
        Favorite favorite = _db.QueryFirstOrDefault<Favorite>(sql, new {favoriteId});
        return favorite;
    }

    internal void RemoveFavorite(string userId, int favoriteId)
    {
        string sql = @"DELETE FROM favorites WHERE id = @favoriteId LIMIT 1
        ;";
        _db.Execute(sql, new{favoriteId});
    }
}
