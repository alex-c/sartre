<template>
  <div id="profile-administration" class="administration-page">
    <div class="administration-title">Profile</div>
    <div class="administration-section">
      <section>
        <b-field label="Name">
          <b-input :value="profile.name"></b-input>
        </b-field>
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
  name: 'profile-administration',
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
