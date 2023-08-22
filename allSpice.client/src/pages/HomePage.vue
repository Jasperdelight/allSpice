<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <!-- New recipe button -->
      <button class="btn btn" data-bs-toggle="modal" data-bs-target="#newRecipeModal">+</button >
        <!-- Favorite Button -->
        <div class="col-3"><button @click="filterBy ='Mexican'">Mexican</button></div>
        <div class="col-3"><button @click="filterBy =''">All</button></div>
        <div class="col-3"><button @click="getFavoriteRecipes()">Favorites</button></div>
        <!-- Recipe Cards -->
      <div v-for="recipe in recipes" :key="recipe.id" class="col-3 m-4 " >
        <RecipeComponent :recipe="recipe"/>
        
      </div>
    </section>
  </div>
  <ModalCard id="newRecipeModal">
    <template #modalHeader>
      Create New Recipe
    </template>
    <template #modalBody>
      <RecipeModal/>
    </template>
  </ModalCard>
  <!-- <ModalCard id="exampleModal"/> -->
  <ModalCard id="exampleModal">
    <template #modalHeader>
      <h4>{{ activeRecipe?.title }}</h4>
    </template>
    <template #modalBody>
      <RecipeForm/>
    </template>
  </ModalCard>
</template>

<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState";
import {recipesService} from "../services/RecipesService"
import {favoritesService} from "../services/FavoritesService"
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import RecipeForm from "../components/RecipeForm.vue";

export default {
    setup() {
      const filterBy = ref('')
        async function getRecipes() {
            await recipesService.getRecipes();
        }
        async function getFavorites() {
            await favoritesService.getFavorites();
        }
        onMounted(() => getRecipes());
        return {
          filterBy,
            recipes: computed(() =>{
              if(filterBy.value == ''){
                return AppState.Recipes
              } else{
                return AppState.Recipes.filter(r=> r.category == filterBy.value)
              }
            }),
            favorites: computed(() => AppState.favorites),
            activeRecipe: computed(()=> AppState.activeRecipe),
            setActiveRecipe(recipe) {
                recipesService.setActiveRecipe(recipe);
            },
            favoriteRecipe(recipeId) {
                if (AppState.favorites.find(f => f.recipeId == recipeId)) {
                  favoritesService.favoriteRecipe(recipeId);
                }
            },
            getFavoriteRecipes(){
              try{
                const favorites = AppState.favorites

                recipesService.getFavoriteRecipes(favorites)
                  
              } catch(error) {
                  Pop.error(error.message);
                  logger.log(error);
              }
            }
        };
    },
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
