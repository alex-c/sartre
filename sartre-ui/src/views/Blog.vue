<template>
  <div class="page">
    <div class="page-title">{{blog.title}}</div>
  </div>
</template>

<script>
import Api from '../api';

export default {
  name: 'blog',
  props: ['id'],
  data() {
    return { blog: null };
  },
  mounted() {
    Api.getBlog(this.id)
      .then(function(response) {
        return response.json();
      })
      .then(data => (this.blog = data))
      .catch(function(error) {
        console.error(error);
      });
  },
};
</script>

<style lang="scss">
</style>