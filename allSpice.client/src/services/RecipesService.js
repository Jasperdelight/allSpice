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
async addInstructions(instructions, recipeId){
  const res = await api.put(`api/recipes/${recipeId}`, instructions)
  // logger.log(res.data)
  AppState.activeRecipe = new Recipe(res.data)
  const foundRecipe = AppState.Recipes.findIndex(r => r.id == recipeId)
  // logger.log('foundRecipe?', foundRecipe)
  AppState.Recipes.splice(foundRecipe, 1, new Recipe(res.data))
}

}
export const recipesService = new RecipesService()