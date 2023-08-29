<template>
  <div class="container-fluid">
          <section class="row " v-if="activeRecipe">
            <div class="col-md-4 col-12">
              <img class="img-fluid" :src="activeRecipe.img" alt="">
            </div>
            <div class="col-md-8 col-12">
              <section class="row">
                <div class="col-12">
                </div>
              </section>
              <section class="row">
                <div class="col-md-5 col-12 bg-grey mx-2">
                  <h5>Recipe Steps</h5>
                  <p>{{ activeRecipe.instructions }}</p>
                  <form @submit.prevent="addInstructions()" action="" class="d-flex">
                    <input v-model="editableTwo.instructions" id="instructions" type="text" class="form-control">
                    <button class="btn btn-success" type="submit">+</button>
                  </form>
                </div>
                <div class="col-md-5 col-12 bg-grey mx-2">
                  <h5>Ingredients</h5>
                  <div v-for="activeIngredient in activeIngredients" :key="activeIngredient.id">
                    <p> <span>{{ activeIngredient.quantity }}</span> {{activeIngredient.name}}</p>
                  </div>
                    <form  @submit.prevent="addIngredients()" action="" class="d-flex" id="form">
                      <input v-model="editable.quantity" id="quantity" type="text" class="form-control" placeholder="Qty...">
                      <input v-model="editable.name" id="name" type="text" class="form-control" placeholder="Ingr...">
                      <button class="btn btn-success" type="submit"><i class="mdi mdi-plus"></i></button>
                    </form>
                </div>
              </section>
            </div>
          </section>
        </div>
</template>


<script>
import { computed, ref, watch } from "vue";
import { ingredientsService } from "../services/IngredientsService";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";

export default {
  setup(){
    const editable = ref({})
    const editableTwo = ref({})
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
      activeIngredients: computed(()=> AppState.activeIngredients),
      editable,
      editableTwo,
      async addInstructions(){
        // editableTwo.value.recipeId = this.activeRecipe.id
        // editableTwo.value
        const newInstructions = AppState.activeRecipe
        newInstructions.instructions = editableTwo.value.instructions
        // logger.log('editableTwo', editableTwo.value)
        await recipesService.addInstructions(newInstructions, this.activeRecipe.id) 
      },
      async addIngredients(){
        let newIngredient = editable.value
        newIngredient.recipeId = this.activeRecipe.id
        await ingredientsService.addIngredients(newIngredient)
      }
      
    }
  }
}
</script>


<style lang="scss" scoped>

</style>