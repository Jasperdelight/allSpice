<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-3 m-4 " >
        <RecipeComponent :recipe="recipe"/>
        

      </div>
    </section>
  </div>
      <!-- <button @click="setActiveRecipe(recipe)" class="btn btn-outline" data-bs-toggle="modal" data-bs-target="#exampleModal">
        {{ recipe.title }}
      </button> -->

    <Modal />
</template>

<script>
import { computed, onMounted, watchEffect } from "vue";
import { AppState } from "../AppState";
import {recipesService} from "../services/RecipesService"
import {favoritesService} from "../services/FavoritesService"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
  setup() {
    async function getRecipes(){
      await recipesService.getRecipes();
    }
    async function getFavorites(){
      await favoritesService.getFavorites()
    }
    onMounted(()=> 
    getRecipes()
    ) 
    return {
      recipes: computed(()=> AppState.Recipes),
      favorites: computed(()=> AppState.favorites),
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

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }

  }

}
.recipe-img{
      max-height: 200px;
      min-width:200px;
      height: 100%;
      width: 100%;
      object-position: center;
      

    }
</style>
