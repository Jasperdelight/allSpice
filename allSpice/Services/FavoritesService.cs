using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _favoritesRepository;

    public FavoritesService(FavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    internal Favorite CreateFavorite(Favorite fData)
    {
        Favorite favorite = _favoritesRepository.CreateFavorite(fData);
        Favorite favoriteObj = GetFavoriteById(fData.Id);
        favorite.Recipe = favoriteObj.Recipe;
        favorite.Creator = favoriteObj.Creator;

        return favorite;
    }

    internal List<Favorite> GetAccountFavorites(string accountId)
    {
        List<Favorite> favorites = _favoritesRepository.GetAccountFavorites(accountId);
        return favorites;
    }

    internal void RemoveFavorite(string userId, int favoriteId)
    {
      Favorite favorite = GetFavoriteById(favoriteId);
      _favoritesRepository.RemoveFavorite(userId, favoriteId);
  
    }

    private Favorite GetFavoriteById(int favoriteId)
    {
      Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
      if(favorite == null)
      {
        throw new Exception("bad id");
      }
      return favorite;
    }
}
