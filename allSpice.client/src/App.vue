<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <ModalCard id="newRecipeModal">
    <template  #modalHeader>
      <h2 class="text-center">
        Create New Recipe
      </h2>
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
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      activeRecipe: computed(()=> AppState.activeRecipe
      )
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
