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
  async addIngredients(iData){
    const res = await api.post(`api/ingredients`, iData)
    AppState.activeIngredients.push(new Ingredient(res.data))
    logger.log(res.data)
  }
}
export const ingredientsService = new IngredientsService()