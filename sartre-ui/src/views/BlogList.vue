<template>
  <div class="page">
    <div class="page-title">Available blogs</div>
    <div id="blog-list">
      <h1>Blog List</h1>
      <div class="tile is-parent">
        <BlogPreview v-for="blog in blogs" v-bind:key="blog.id" :blog="blog" />
      </div>
    </div>
  </div>
</template>

<script>
import Api from '../api';
import BlogPreview from '@/components/BlogPreview.vue';

export default {
  name: 'blog-list',
  components: { BlogPreview },
  data() {
    return {
      blogs: [],
    };
  },
  mounted() {
    Api.getBlogList()
      .then(response => (this.blogs = response.data))
      .catch(response => console.error(response.data.message));
  },
};
</script>

<style lang="scss"></style>
