<template>
<form action="" @submit.prevent="createRecipe()">
  <div class="form-group d-flex">
    <div class="col-6">
      <label for="title" class="">Title</label>
      
      <input v-model="editable.title" class="form-control" type="text" placeholder="title.." id="title" required>
    </div>
    <div class="col-6">

      <!-- <div class="input-group">
  <input type="text" class="form-control" aria-label="Text input with dropdown button">
  <div class="input-group-append">
    <button class="btn btn-outline-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Dropdown</button>
    <div class="dropdown-menu">
      <a class="dropdown-item" href="#">Action</a>
      <a class="dropdown-item" href="#">Another action</a>
      <a class="dropdown-item" href="#">Something else here</a>
      <div role="separator" class="dropdown-divider"></div>
      <a class="dropdown-item" href="#">Separated link</a>
    </div>
  </div>
</div> -->

      <label for="category" class="">Category</label>
      <select v-model="editable.category" class="form-control" placeholder="category.." id="category" required>
        <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
      </select>
    </div>
  </div>
  <div class="form-group">
    <label for="details" class="">Details</label>
    <input v-model="editable.details" class="form-control" type="text" placeholder="details.." id="details" required>
  </div>
  <div class="form-group">
    <label for="img" class="">Image</label>
    <input v-model="editable.img" class="form-control" type="url" placeholder="image.." id="img" required>
  </div>
  <div class="d-flex justify-content-end p-2">
    <button class="btn btn-success text-end">+</button>
  </div>
</form>
</template>


<script>
import { computed, ref } from "vue";
import { logger } from "../utils/Logger";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop";
import { recipesService } from "../services/RecipesService";

export default {
  setup(){
    const editable = ref({})
    return {
      editable,
      categories: computed(()=> ['Pasta', 'Soup', 'Fish', 'Salads', 'Beef', 'Burgers', 'Misc', 'Cheese', 'Italian', 'Mexican', 'Specialty Coffee']),
      
      async createRecipe(){
        try{
          const formData = editable.value
          // logger.log(formData,'form submitted')
          await recipesService.createRecipe(formData)
          editable.value = {}
          Modal.getOrCreateInstance('#newRecipeModal').hide()
        } catch (error){
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>