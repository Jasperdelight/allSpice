<template>
  <div class="container-fluid">
    <section class="row justify-content-center text-center">
      <h1 class="text-center">AllSpice</h1>
      <!-- New recipe button -->
        <!-- Favorite Button -->
        <div class="col-3 "><button class="btn btn-outline" @click="filterBy ='Mexican'">Mexican</button></div>
        <div class="col-3 "><button class="btn btn-outline" @click="filterBy ='Italian'">Italian</button></div>
        <div class="col-3"><button class="btn btn-outline" @click="filterBy = 'Cheese'">Cheese</button></div>
        <div class="col-3"><button class="btn btn-outline" @click="filterBy =''">All</button></div>
        <!-- Recipe Cards -->
      <div v-for="recipe in recipes" :key="recipe.id" class="col-3" >
        <RecipeComponent :recipe="recipe"/>
        
      </div>
    </section>
  </div>

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
      const filterFavorite = ref('')
        async function getRecipes() {
            await recipesService.getRecipes();
        }

        onMounted(() => getRecipes());
        return {
          filterFavorite,
          favorites: computed(()=>{
            if (filterFavorite.value == '') {
              return AppState.Recipes
            }else{
              return AppState.Recipes == AppState.favorites.find(f => f.recipeId == AppState.Recipes.forEach(r=> r.id == f.recipeId))
            }
          }),
          
          filterBy,
            recipes: computed(() =>{
              if(filterBy.value == ''){
                return AppState.Recipes
              } else{
                return AppState.Recipes.filter(r=> r.category == filterBy.value)
              }
            }),
            // favorites: computed(() => AppState.favorites),
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
                // logger.log(favorites)
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
