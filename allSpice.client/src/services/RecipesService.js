import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {

  async getRecipes(){
    const res = await api.get("api/recipes")
    AppState.Recipes = res.data.map(r => new Recipe(r))
    // logger.log(AppState.Recipes, "recipes")
  }
  setActiveRecipe(recipe){
    AppState.activeRecipe = new Recipe(recipe)
  }
}
export const recipesService = new RecipesService()