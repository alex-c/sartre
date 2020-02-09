import Vue from 'vue';
import Router from 'vue-router';
import Blog from './views/Blog.vue';
import BlogList from './views/BlogList.vue';
import Login from './views/Login.vue';
import Administration from './views/Administration.vue';
import ProfileAdministration from './components/ProfileAdministration.vue';
import PlatformAdministration from './components/PlatformAdministration.vue';
import Api from './api';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      beforeEnter: function(_to, _from, next) {
        Api.getHomePage()
          .then(response => {
            if (response.data.type === 0) {
              next({ path: `/blogs/${response.data.data.id}` });
            } else {
              next({ path: '/blogs' });
            }
          })
          .catch(_ => next({ path: '/blogs' }));
      },
    },
    {
      path: '/blogs',
      name: 'blogs',
      component: BlogList,
    },
    {
      path: '/blogs/:id',
      name: 'blog',
      component: Blog,
      props: true,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/admin',
      component: Administration,
      beforeEnter: function(_to, _from, next) {
        if (localStorage.getItem('token') === null) {
          next({ path: '/' });
        } else {
          next();
        }
      },
      children: [
        {
          path: 'profile',
          component: ProfileAdministration,
        },
        {
          path: 'platform',
          component: PlatformAdministration,
        },
      ],
    },
  ],
});
