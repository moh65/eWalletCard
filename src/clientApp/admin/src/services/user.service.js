import  * as config from './config';

export const userService = {
    login,
    logout,
};

function login({ username, password }) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };
    return fetch(`${config.baseUrl}api/v1/user/auth`, requestOptions)
        .then(handleResponse)
        .then(user => {
            console.log(user);
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user.token));
            localStorage.setItem('username', username);

            return user.token;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
    localStorage.removeItem('username');
}


function handleResponse(response) {
    return response.json().then(data => {
        
      
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                //logout();
                return Promise.reject({ logout : true });

               
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        if(!data.result && !data.result.token) {
            const error = (data && data.message) || response.message || data.Message ;
            return Promise.reject(error);
        }
        return data.result;
    });
}