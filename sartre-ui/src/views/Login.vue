<template>
  <div id="login">
    <div id="login-title">
      Sartre - Login
      <div id="return-home">
        <router-link to="/">
          <b-icon icon="arrow-left" type="is-danger"></b-icon>
        </router-link>
      </div>
    </div>
    <div id="login-box">
      <b-field
        label="Login Name"
        :type="{ 'is-danger': errors.loginIsEmpty }"
        :message="{ 'Please enter a login name.': errors.loginIsEmpty }"
      >
        <b-input v-model="form.login" icon="account" @blur="validateLoginName"></b-input>
      </b-field>
      <b-field
        label="Password"
        :type="{ 'is-danger': errors.passwordIsEmpty }"
        :message="{ 'Please enter a password.': errors.passwordIsEmpty }"
      >
        <b-input type="password" v-model="form.password" icon="lock" @blur="validatePassword"></b-input>
      </b-field>
      <b-button id="login-button" type="is-success" icon-left="login" @click="login">Login</b-button>
    </div>
    <div id="login-footer">
      Built with
      <a href="https://www.github.com/alex-c/sartre">Sartre.</a>
    </div>
  </div>
</template>

<script>
import Api from '../api';

export default {
  name: 'login',
  data() {
    return {
      form: {
        login: '',
        password: '',
      },
      errors: {
        loginIsEmpty: false,
        passwordIsEmpty: false,
      },
    };
  },
  methods: {
    validateLoginName() {
      let valid = this.form.login !== '';
      this.errors.loginIsEmpty = !valid;
      return valid;
    },
    validatePassword() {
      let valid = this.form.password !== '';
      this.errors.passwordIsEmpty = !valid;
      return valid;
    },
    validateForm() {
      let validLoginName = this.validateLoginName();
      let validPassword = this.validatePassword();
      return validLoginName && validPassword;
    },
    login() {
      if (this.validateForm()) {
        Api.login(this.form.login, this.form.password)
          .then(response => {
            localStorage.setItem('token', response.data.token);
            this.$router.push({ path: '/admin' });
          })
          .catch(response => {
            if (response.status === 401) {
              this.form.password = '';
              this.$buefy.notification.open({
                duration: 2500,
                message: `Bad login credentials.`,
                position: 'is-top-right',
                type: 'is-danger',
                hasIcon: true,
              });
            }
          });
      }
    },
  },
};
</script>

<style lang="scss" scoped>
#login {
  margin: auto;
  margin-top: 36px;
  text-align: left;
  width: 500px;
}

#login-title {
  font-size: 24px;
  margin-bottom: 4px;
}

#return-home {
  float: right;
}

#login-box {
  border: 1px solid #3c3c3c;
  border-radius: 3px;
  padding: 24px;
  box-shadow: 0px 0px 3px #6c6c6c;
  overflow: auto;
}

#login-button {
  margin-top: 8px;
  float: right;
}

#login-footer {
  text-align: center;
  padding: 4px;
  color: #3c3c3c;
}
</style>