<template>
  <div class="home">
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
