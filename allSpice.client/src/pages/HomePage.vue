<template>
  <div class="container-fluid">
    <section class="row justify-content-center text-center">
      <h2 class="text-center" >Recipes</h2>
      <!-- New recipe button -->
        <!-- Favorite Button -->
        <div class="d-flex justify-content-around">
          <div class=""><button class="btn btn-outline" @click="filterBy =''">All</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Pasta'">Pasta</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Salads'">Salads</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Beef'">Beef</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Burgers'">Burgers</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Specialy Coffee'">Coffee</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Soup'">Soup</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Mexican'">Mexican</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Italian'">Italian</button></div>
          <div class=""><button class="btn btn-outline" @click="filterBy = 'Cheese'">Cheese</button></div>
          <div class=" "><button class="btn btn-outline" @click="filterBy ='Misc'">Misc</button></div>
        </div>
        <!-- Recipe Cards -->
        <div class="gallery-container">

          <figure v-for="recipe in recipes" :key="recipe.id" class="elevation-4 rounded my-2" >
            <RecipeComponent :recipe="recipe"/>
            
          </figure>
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
.container {
   max-width: 1224px;
   margin: 0 auto;
}

img {
   width: 500px;
   object-fit: contain;
   border-radius: 15px;
}

.gallery-container {
  @media only screen 
and (max-width : 390px){
  column-count: 2;
}
@media only screen 
and (min-width : 1224px){
  column-count: 4;
}
column-count: 2;
column-gap: 20px 20px;
 width: 100%;
}
.gallery-container2 {
 column-count: 2;
 column-gap: 20px 20px;
 width: 1200px;
}

figure {
 margin: 0;
 display: inline-block;
 margin-bottom: 0px;
 width: 100%;
}


.recipe-img{
      max-height: 200px;
      min-width:200px;
      height: 100%;
      width: 100%;
      object-position: center;
      

    }
</style>
