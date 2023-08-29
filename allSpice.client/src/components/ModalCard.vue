<template>
  <div  class="modal fade modal-xl" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="min-width: 90%;">
  <div class="modal-dialog" role="document">
    <div  class="modal-content">
      <div  class="modal-header modal-xl">
        <!-- <h5  class="modal-title" id="modalHeader">{{ activeRecipe.title }}</h5> -->

          <slot  name="modalTitle"></slot>
        <slot class="d-flex justify-content-center" name="modalHeader"></slot>
        <button type="button" class="close btn" data-bs-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body modal-xl">
        <slot name="modalBody"></slot>

      </div>
      <div class="modal-footer">

      </div>
    </div>
  </div>
</div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState";




export default {
  setup(){
    return {
      activeRecipe: computed(()=>AppState.activeRecipe)
    }
  }
}
</script>


<style lang="scss" scoped>

</style>