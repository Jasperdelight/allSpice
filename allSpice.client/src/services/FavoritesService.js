import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService{

  async favoriteRecipe(recipeId){
    const res = await api.post('api/favorites', {recipeId})
    AppState.favorites.push(new Favorite(res.data))
    // logger.log(res.data)
  }
  async getFavorites(){
    const res = await api.get("account/favorites")
    // AppState.favorites = res.data.map(f=> f)
    AppState.favorites = res.data.map(f=> new Favorite(f))
    // logger.log(AppState.favorites)
    // const favoriteId = AppState.favorites.
    // AppState.Recipes.find(r=> r.id == res.data)
  }
}
export const favoritesService = new FavoritesService()