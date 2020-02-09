function processResponse(response) {
  return new Promise((resolve, reject) => {
    if (response.status === 401) {
      reject({ status: response.status });
    } else {
      let handler;
      response.status < 400 ? (handler = resolve) : (handler = reject);
      response.json().then(data => handler({ status: response.status, data: data }));
    }
  });
}

export default {
  login: (loginName, password) => {
    return fetch('http://localhost:5000/api/auth', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Login: loginName, Password: password }),
    }).then(processResponse);
  },
  getHomePage: () => {
    return fetch('http://localhost:5000/api/sartre').then(processResponse);
  },
  getBlogList: () => {
    return fetch('http://localhost:5000/api/blogs').then(processResponse);
  },
  getBlog: id => {
    return fetch(`http://localhost:5000/api/blogs/${id}`).then(processResponse);
  },
  getUserProfile: login => {
    return fetch(`http://localhost:5000/api/users/${login}`).then(processResponse);
  },
};
