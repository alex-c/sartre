<template>
  <div class="home">
    <BlogList />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import BlogList from '@/components/BlogList.vue';

@Component({
  components: {
    BlogList,
  },
})
export default class Home extends Vue {}
</script>
