<template>
  <div v-if="activeRecipe" class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">{{ activeRecipe.title }}</h5>
        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        {{activeRecipe.instructions}}
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
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
    }
  }
}
</script>


<style lang="scss" scoped>

</style>