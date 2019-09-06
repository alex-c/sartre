<template>
  <div id="blog-list">
    <h1>Blog List</h1>
    <BlogPreview v-for="blog in blogs" v-bind:key="blog.id" />
  </div>
</template>

<script>
import Api from '../api';
import BlogPreview from '@/components/BlogPreview.vue';

export default {
  name: 'blog-list',
  components: {
    BlogPreview,
  },
  data() {
    return {
      blogs: [],
    };
  },
  mounted() {
    Api.getBlogList()
      .then(function(response) {
        return response.json();
      })
      .then(data => (this.blogs = data))
      .catch(function(error) {
        console.error(error);
      });
  },
};
</script>

<style lang="scss">
</style>