<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <button v-for="recipe in recipes" :key="recipe.id" class="col-3 m-4 btn btn-outline bg-grey" data-bs-toggle="modal" data-bs-target="#exampleModal">
        <h5 class="d-flex justify-content-between">{{ recipe.title }} <i class="mdi mdi-heart"></i></h5>
        <img :src="recipe.img" class="img-fluid recipe-img" alt=""  @click="setActiveRecipe(recipe)" >

      </button>
    </section>
  </div>
      <!-- <button @click="setActiveRecipe(recipe)" class="btn btn-outline" data-bs-toggle="modal" data-bs-target="#exampleModal">
        {{ recipe.title }}
      </button> -->

    <Modal />
</template>

<script>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState";
import {recipesService} from "../services/RecipesService"
import { logger } from "../utils/Logger";

export default {
  setup() {
    async function getRecipes(){
      await recipesService.getRecipes();
      
    }
    onMounted(()=> getRecipes())
    return {
      recipes: computed(()=> AppState.Recipes),
      setActiveRecipe(recipe){
        recipesService.setActiveRecipe(recipe)
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
