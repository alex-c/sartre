import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

const jwtDecode = require('jwt-decode');

export default new Vuex.Store({
  state: {
    platformName: '',
    token: null,
    userName: '',
    userRoles: [],
  },
  mutations: {
    setPlatformName(state, platformName) {
      state.platformName = platformName;
    },
    login(state, token) {
      // Save token
      state.token = token;
      localStorage.setItem('token', token);

      // Save parsed data
      let decodedToken = jwtDecode(token);
      state.userName = decodedToken.sub;
      state.userRoles = decodedToken.role;
    },
    logout(state) {
      state.token = null;
      state.userName = '';
      state.userRoles = [];
      localStorage.removeItem('token');
    },
  },
  actions: {},
  modules: {},
});
