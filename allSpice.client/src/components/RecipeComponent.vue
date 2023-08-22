<template>
  <h5  class="d-flex justify-content-between">{{ recipe.title }} 
    <div>
      <i v-if="!isFavorited" @click="favoriteRecipe(recipe.id)" class="mdi mdi-heart-outline selectable"></i>
      <i v-else @click="favoriteRecipe(recipe.id)" class="mdi mdi-heart selectable"></i>
    </div>
  </h5>
  <button class="btn btn-outline bg-grey " data-bs-toggle="modal" data-bs-target="#exampleModal">
  <img :src="recipe.img" class="img-fluid recipe-img" alt=""  @click="setActiveRecipe(recipe)" >
  </button>

</template>


<script>
import { computed } from "vue";
import { Recipe } from "../models/Recipe";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import { favoritesService } from "../services/FavoritesService";
import Pop from "../utils/Pop";

export default {
  props:{
      recipe:{type: Recipe, required: true}
    },
  setup(props){
    

    return {
      props,
      recipes: computed(()=> AppState.Recipes),
      favorites: computed(()=> AppState.favorites),
      isFavorited:computed(()=> AppState.favorites.find(f => f.recipeId == props.recipe.id )),
      favoriteRecipes:computed(()=> AppState.favorites.find(f => f.recipeId == props.recipe.id )),
      setActiveRecipe(recipe){
        recipesService.setActiveRecipe(recipe)
      },
      favoriteRecipe(recipeId){
        if (AppState.favorites.find(f=> f.recipeId == recipeId)) {
          Pop.error('Already Favorited This Recipe')
          return
        }
        favoritesService.favoriteRecipe(recipeId)
      }

      
    }
  }
}
</script>


<style lang="scss" scoped>

</style>