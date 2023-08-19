import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{

  async  getIngredients(){
    let recipeId = AppState.activeRecipe?.id
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log(res.data)
  }
}
export const ingredientsService = new IngredientsService()