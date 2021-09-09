import  * as config from './config';
import {authHeader} from "../helpers/authHeader";

export const cardService = {
    register,
    activate
};

function register(data) {
    const auth = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' , ...auth },
        body: JSON.stringify(data),
    };
    return fetch(`${config.baseUrl}api/v1/card/register`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}


function activate(data) {
    const auth = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' , ...auth },
        body: JSON.stringify(data),
    };
    return fetch(`${config.baseUrl}api/v1/card/Activate`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}



function handleResponse(response) {

    return response.json().then(data => {
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                //logout();
                return Promise.reject({ logout : true });
            }
            const error = (data && data.message) ||  data.Message || response.statusText ;
            return Promise.reject(error);
        }

        if(!data.result && !data.isSuccess) {
            const error = (data && data.message)||  data.Message || response.statusText ;
            return Promise.reject(error);
        }

        return data?.result;
    });

}