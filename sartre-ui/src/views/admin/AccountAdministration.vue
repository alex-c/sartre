<template>
  <div id="profile-administration" class="administration-page">
    <nav class="breadcrumb" aria-label="breadcrumbs">
      <ul>
        <li>
          <router-link to="/admin">Administration</router-link>
        </li>
        <li class="is-active">
          <a href="#">Identity</a>
        </li>
        <li class="is-active">
          <a href="#" aria-current="page">Account</a>
        </li>
      </ul>
    </nav>
    <div class="administration-section">
      <section>
        <b-field label="Name">Mz Name</b-field>
        <b-field label="Biography">
          <b-input
            type="textarea"
            minlength="0"
            maxlength="1000"
            placeholder="A short introduction..."
            :value="profile.bio"
          ></b-input>
        </b-field>
        <b-field label="Website">
          <b-input placeholder="URL" type="url"></b-input>
        </b-field>
        <b-button
          id="save-profile-button"
          type="is-success"
          icon-left="content-save"
          @click="saveProfile"
        >Save</b-button>
      </section>
    </div>
  </div>
</template>

<script>
import Api from '../../api';

export default {
  name: 'account-administration',
  data() {
    return {
      profile: {
        name: '',
        bio: '',
        link: '',
      },
    };
  },
  mounted() {
    Api.getUserProfile(this.$store.state.userName)
      .then(response => {
        this.profile.name = response.data.name;
        this.profile.bio = response.data.biography;
        this.profile.link = response.data.website;
      })
      .catch(response => {
        //TODO?
      });
  },
  methods: {
    saveProfile() {},
  },
};
</script>

<style lang="scss" scoped></style>
