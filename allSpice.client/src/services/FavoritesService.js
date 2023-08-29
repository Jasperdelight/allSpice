import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService{

  async favoriteRecipe(recipeId){
    const res = await api.post('api/favorites', {recipeId})
    AppState.favorites.push(new Favorite(res.data))
    
    // logger.log(AppState.favorites)
  }
  async getFavorites(){
    const res = await api.get("account/favorites")
    // AppState.favorites = res.data.map(f=> f)
    AppState.favorites = res.data.map(f=> new Favorite(f))
    // logger.log(AppState.favorites)
    // const favoriteId = AppState.favorites.
    // AppState.Recipes.find(r=> r.id == res.data)
  }

  async unFavoriteRecipe(recipeId){
    const foundFavorite = AppState.favorites.find(f => f.recipeId == recipeId)
    // logger.log(foundFavorite.id)
    const favoriteIndex = AppState.favorites.findIndex(f=> f.recipeId == recipeId)
    const favoriteId = foundFavorite.id
    const res = await api.delete(`api/favorites/${favoriteId}`)
    AppState.favorites.splice(favoriteIndex, 1)

  }
}
export const favoritesService = new FavoritesService()