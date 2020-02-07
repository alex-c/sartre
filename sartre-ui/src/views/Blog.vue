<template>
  <div class="page" v-if="blog != null">
    <div class="page-title">{{ blog.title }}</div>
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
      .then(response => (this.blog = response.data))
      .catch(response => console.error(response.data.message));
  },
};
</script>

<style lang="scss"></style>
