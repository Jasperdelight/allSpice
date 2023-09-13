<template>
  <figcaption>

    <h5  class="d-flex justify-content-between">{{ recipe.title }} 
      <div>
        <i v-if="!isFavorited && account.id" @click="favoriteRecipe(recipe.id)" class="mdi mdi-heart-outline selectable"></i>
        <i v-if="isFavorited" @click="unFavoriteRecipe(recipe.id)" class="mdi mdi-heart selectable"></i>
      </div>
    </h5>
    <div class="" data-bs-toggle="modal" data-bs-target="#exampleModal">
      <img :src="recipe.img" class="img-fluid recipe-img" alt=""  @click="setActiveRecipe(recipe)" >
    </div>
  </figcaption>

</template>


<script>
import { computed } from "vue";
import { Recipe } from "../models/Recipe";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import { favoritesService } from "../services/FavoritesService";
import Pop from "../utils/Pop";
import { logger } from "../utils/Logger";

export default {
  props:{
      recipe:{type: Object, required: true}
    },
  setup(props){
    

    return {
      props,
      recipes: computed(()=> AppState.Recipes),
      favorites: computed(()=> AppState.favorites),
      account: computed(()=> AppState.account),
      isFavorited:computed(()=> AppState.favorites.find(f => f.recipeId == props.recipe.id )),
      favoriteRecipes:computed(()=> AppState.favorites.find(f => f.recipeId == props.recipe.id )),
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
figure {
  margin: 0;
  display: grid;
  // grid-template-rows: 1fr auto;
  background-color: white;
}

figure > img {
  grid-row: 1 / -1;
  grid-column: 1;
}

figure a {
  color: black;
  text-decoration: none;
}

figcaption {
  grid-row: 2;
  grid-column: 1;
  background-color: rgba(255, 255, 255, 0);
  padding: .2em .5em;
  justify-self: start;
  position: relative;

  // display: flex;
  // bottom: 50px;
}

.container {
  display: grid;
  gap: 10px;
  grid-template-columns: 1fr 2fr;
  // height: 200vh;
  align-tracks: inherit ;
  align-content: start;
}
figcaption .avatar {
  display: flex;
  align-items: center;
}
.avatar{
  height: 40px;
  width: 40px;
  border-radius: 50%;
  object-fit: cover;
  position: relative;
}
.glassbox{
  background-color: rgba(255, 255, 255, 0.658);
}
</style>