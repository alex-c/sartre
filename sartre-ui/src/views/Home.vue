<template>
  <div class="page">
    <div class="page-title">Blogs</div>
    <BlogList />
  </div>
</template>

<script>
import BlogList from '@/components/BlogList.vue';

export default {
  name: 'home',
  components: {
    BlogList,
  },
};
</script>

<style lang="scss">
</style>