<template>
    <div v-for="recipe in recipes" :key="recipe.id">
      <button @click="setActiveRecipe(recipe)" class="btn btn-outline" data-bs-toggle="modal" data-bs-target="#exampleModal">
        {{ recipe.title }}
      </button>
    </div>
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
</style>
