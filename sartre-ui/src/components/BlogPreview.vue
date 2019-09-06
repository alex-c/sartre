<template>
  <div class="tile is-parent is-child">
    <router-link :to="{name: 'blog', params: {id: blog.id}}">
      <div class="tile is-child box">
        <div class="box-title">{{blog.title}}</div>
        <span>{{blog.description}}</span>
      </div>
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'blog-preview',
  props: ['blog'],
};
</script>

<style lang="scss">
</style>