function processResponse(response) {
  return new Promise((resolve, reject) => {
    let handler;
    response.status < 400 ? (handler = resolve) : (handler = reject);
    response.json().then(data => handler({ status: response.status, data: data }));
  });
}

export default {
  getHomePage: () => {
    return fetch('http://localhost:5000/api/sartre').then(processResponse);
  },
  getBlogList: () => {
    return fetch('http://localhost:5000/api/blogs').then(processResponse);
  },
  getBlog: id => {
    return fetch(`http://localhost:5000/api/blogs/${id}`).then(processResponse);
  },
};
