export default {
  getBlogList: () => {
    return fetch('http://localhost:5000/api/blogs');
  },
};
