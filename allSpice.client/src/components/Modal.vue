<template>
  <div v-if="activeRecipe" class="modal fade modal-xl" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="min-width: 90%;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header modal-xl">
        <h5 class="modal-title" id="exampleModalLabel">{{ activeRecipe.title }}</h5>
        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body modal-xl">
        <div class="container-fluid">
          <section class="row">
            <div class="col-4">
              <img class="img-fluid" :src="activeRecipe.img" alt="">
            </div>
            <div class="col-8">
              <section class="row">
                <div class="col-12">
                  <!-- <h4>{{ activeRecipe.title }}</h4> -->
                </div>
              </section>
              <section class="row">
                <div class="col-5 bg-grey mx-2">
                  <h5>Recipe Steps</h5>
                  <p>{{ activeRecipe.instructions }}</p>
                  <form action="">
                    <input type="text" class="form-control">
                  </form>
                </div>
                <div class="col-5 bg-grey mx-2">
                  <h5>Ingredients</h5>
                  <div v-for="activeIngredient in activeIngredients" :key="activeIngredient.id">
                    <p> <span>{{ activeIngredient.quantity }}</span> {{activeIngredient.name}}</p>
                  </div>
                    <form action="" class="">
                      <input type="text" class="form-control">
                    </form>
                </div>
              </section>
            </div>
          </section>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> 
      </div>
    </div>
  </div>
</div>
</template>


<script>
import { computed, onMounted, watch, watchEffect } from "vue";
import { AppState } from "../AppState";
import {ingredientsService} from "../services/IngredientsService"


export default {


  setup(){
    async function getIngredients(){
    await ingredientsService.getIngredients()
  }
  watch(
      () => AppState.activeRecipe,
      async(newActiveRecipe) => {
        if (newActiveRecipe !== null) {
          await getIngredients();
        }
      }
    );
    
    return {
      activeRecipe: computed(()=> AppState.activeRecipe),
      activeIngredients: computed(()=> AppState.activeIngredients)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>