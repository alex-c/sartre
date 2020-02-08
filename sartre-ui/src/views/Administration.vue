<template>
  <div id="administration">
    <div id="navbar">
      <div id="navbar-title">Sartre Administration</div>
      <div id="navbar-buttons">
        <b-button type="is-danger" icon-left="logout">Logout</b-button>
      </div>
    </div>
    <div id="content">
      <div id="menu">
        <b-menu>
          <b-menu-list label="Account">
            <b-menu-item
              icon="account-card-details"
              label="Profile"
              tag="router-link"
              to="/admin/profile"
            ></b-menu-item>
          </b-menu-list>
          <b-menu-list label="Content"></b-menu-list>
          <b-menu-list label="Administration">
            <b-menu-item icon="settings" label="Platform" tag="router-link" to="/admin/platform"></b-menu-item>
          </b-menu-list>
        </b-menu>
      </div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: 'administration',
  data() {
    return {
      page: 'profile',
    };
  },
};
</script>

<style lang="scss" scoped>
#administration {
  text-align: left;
}

#navbar {
  position: sticky;
  top: 0px;
  overflow: auto;
  padding: 16px;
  background-color: hsl(217, 71%, 53%);
  border-bottom: 1px solid hsl(217, 69%, 41%);
  color: white;
  margin-bottom: 4px;
  box-shadow: 0px 0px 5px black;
}

#navbar-title {
  float: left;
  font-size: 24px;
}

#navbar-buttons {
  float: right;
}

#content {
  overflow: auto;
}

#menu {
  width: 20%;
  min-width: 200px;
  float: left;
  padding: 8px;
}
</style>