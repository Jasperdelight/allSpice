<template>
  <div>

    <h5  class="d-flex justify-content-between">{{ favorite.recipe?.title }} 
      <div>
        <i v-if="!isFavorited" @click="favoriteRecipe(favorite.recipe?.id)" class="mdi mdi-heart-outline selectable"></i>
        <i v-else @click="unFavoriteRecipe(favorite.recipe?.id)" class="mdi mdi-heart selectable"></i>
      </div>
    </h5>
    <button class="btn btn-outline bg-grey " data-bs-toggle="modal" data-bs-target="#exampleModal">
      <img :src="favorite.recipe?.img" class="img-fluid recipe-img" alt=""  @click="setActiveRecipe(favorite.recipe)" >
    </button>
    
  </div>
</template>


<script>
import { computed } from "vue";
import { Recipe } from "../models/Recipe";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import { favoritesService } from "../services/FavoritesService";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";
import { Favorite } from "../models/Favorite";

export default {
  props:{
      favorite:{type: Favorite, required: true}
    },
  setup(props){
    

    return {
      props,
      recipes: computed(()=> AppState.Recipes),
      favorites: computed(()=> AppState.favorites),
      isFavorited:computed(()=> AppState.favorites.find(f => f.recipeId == props.favorite.recipe.id )),
      favoriteRecipes:computed(()=> AppState.favorites.find(f => f.recipeId == props.favorite.recipe.id )),
      setActiveRecipe(recipe){
        recipesService.setActiveRecipe(recipe)
      },
      favoriteRecipe(recipeId){
        // if (AppState.favorites.find(f=> f.recipeId == recipeId)) {
        //   Pop.error('Already Favorited This Recipe')
        //   return
        // }
        try{
          // logger.log(recipeId)
          favoritesService.favoriteRecipe(recipeId)
        } catch(error) {
            Pop.error(error.message);
        }
      },
      unFavoriteRecipe(recipeId){
        try{
            // logger.log(recipeId)
            favoritesService.unFavoriteRecipe(recipeId)
        } catch(error) {
            Pop.error(error.message);
        }
      }
      
    }
  }
}
</script>


<style lang="scss" scoped>

</style>