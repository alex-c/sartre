import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    platformName: '',
  },
  mutations: {
    setPlatformName(state, platformName) {
      state.platformName = platformName;
    },
  },
  actions: {},
  modules: {},
});
