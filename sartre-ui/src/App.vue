<template>
  <div id="app">
    <router-view />
  </div>
</template>

<style lang="scss">
@import '~bulma/sass/utilities/_all';

// Add Buefy customization here!

@import '~bulma';
@import '~buefy/src/scss/buefy';

body {
  text-align: center;
}

#app {
  margin: auto;
  font-family: 'Martel', serif;
  font-size: 12px;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

.page {
  text-align: left;
}

.page-title {
  font-size: 2rem;
  padding: 1rem;
}

.box-title {
  font-size: 1.5rem;
  font-weight: 600;
  margin-bottom: 1rem;
}
</style>
