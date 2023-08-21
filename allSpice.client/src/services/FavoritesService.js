import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService{

  async favoriteRecipe(recipeId){
    const res = await api.post('api/favorites', {recipeId})
    AppState.favorites.push(res.data)
    logger.log(res.data)
  }
  async getFavorites(){
    const res = await api.get("account/favorites")
    logger.log(res.data)
    // AppState.favorites = res.data.map(f=> f)
    AppState.favorites = res.data
  }
}
export const favoritesService = new FavoritesService()