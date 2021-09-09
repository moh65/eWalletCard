import  * as config from './config';
import {authHeader} from '../helpers/authHeader'

export const clientService = {
    list,
};

function list(skip , take , nationalCode = "", mobile = "") {
    var header = authHeader();
    const requestOptions = {
        method: 'GET',
        headers: { ...header },
    };

    return fetch(`${config.baseUrl}api/v1/client/list?skip=${skip}&take=${take}&nationalCode=${nationalCode}&Mobile=${mobile}` , requestOptions)
        .then(handleResponse)
        .then(clients => {
            return clients;
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

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        if(!data.result) {
            const error = (data && data.message) || response.message  || data.Message;
            return Promise.reject(error);
        }

        return data.result;
    });
}