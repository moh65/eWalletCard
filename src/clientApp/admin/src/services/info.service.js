import  * as config from './config';
import { authHeader  } from "../helpers/authHeader";
export const infoService = {
    loadData,
};

function loadData(nationalCode) {
    var auth = authHeader()
    const requestOptions = {
        method: 'GET',
        headers: { ...auth } ,
    };
    return fetch(`${config.baseUrl}api/v1/client/info?nationalCode=${nationalCode}`, requestOptions)
        .then(handleResponse)
        .then(client => {
            
            return client;
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

            const error = (data && data.message) || data.message || data.Message;
            return Promise.reject(error);
        }

        if(!data.result) {
            const error = (data && data.message) || response.message || data.Message;
            return Promise.reject(error);
        }
        return data.result;
    });
}