import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

// JWT decoding tool
const jwtDecode = require('jwt-decode');

// Load login state from local storage on startup
let token = localStorage.getItem('token');
let userName = '';
let userRoles = [];
if (token !== null) {
  let decodedToken = jwtDecode(token);
  userName = decodedToken.sub;
  userRoles = decodedToken.role;
}

// Initialize store
export default new Vuex.Store({
  state: {
    platformName: '',
    token: token,
    userName: userName,
    userRoles: userRoles,
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
