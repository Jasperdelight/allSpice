<template>
<div class="container-fluid">
  <section class="row justify-content-center text-center">
    <h1 class="col-12 text-center">
      Favorites
    </h1>
      <div class="col-3 "><button class="btn btn-outline" @click="filterBy ='Mexican'">Mexican</button></div>
        <div class="col-3 "><button class="btn btn-outline" @click="filterBy ='Italian'">Italian</button></div>
        <div class="col-3"><button class="btn btn-outline" @click="filterBy = 'Cheese'">Cheese</button></div>
        <div class="col-3"><button class="btn btn-outline" @click="filterBy =''">All</button></div>
    <div class="col-3" v-for="favorite in favorites" :key="favorite.id" >
      <!-- TODO Need to swap to a different component -->
      <FavoriteRecipeComponent :favorite="favorite"/>
    </div>
  </section>
</div>  
</template>


<script>
import { computed, onMounted, ref } from "vue";
import { favoritesService } from "../services/FavoritesService";
import { AppState } from "../AppState";

export default {
  setup(){
    const filterBy = ref('')
    
    async function getFavorites() {
            await favoritesService.getFavorites();
        }
        onMounted(()=> getFavorites)
    return {
      filterBy,
            favorites: computed(() =>{
              if(filterBy.value == ''){
                return AppState.favorites
              } else{
                return AppState.favorites.filter(r=> r.recipe.category == filterBy.value)
              }
            }),
      // favorites: computed(()=> AppState.favorites)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>