import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{

  async  getIngredients(){
    let recipeId = AppState.activeRecipe?.id
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    // logger.log(res.data.length)
    if (res.data.length == 1) {
      AppState.activeIngredients = new Ingredient(res.data)
    }
    AppState.activeIngredients = res.data.map(i=> new Ingredient(i))
    // logger.log(AppState.activeIngredients, "ingredients")
  }
}
export const ingredientsService = new IngredientsService()