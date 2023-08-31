<template>
<div class="container-fluid">
  <section class="row justify-content-center text-center">
    <h2 class="col-12 text-center">
      Favorites
    </h2>
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
        <div class="gallery-container">

          <figure class="elevation-4 rounded my-2" v-for="favorite in favorites" :key="favorite.id" >
            <FavoriteRecipeComponent :favorite="favorite"/>
          </figure>
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