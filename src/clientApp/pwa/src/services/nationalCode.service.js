import  * as config from './config';
import {authHeader} from "../helpers/authHeader";

export const nationalCodeService = {
    checkNationalCodeInTemporary,

};

function checkNationalCodeInTemporary(nationalCode) {
    console.log(config.baseUrl);
    const auth = authHeader();
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' , ...auth },
    };
    return fetch(`${config.baseUrl}api/v1/client/IsLegal?nationalCode=${nationalCode}`, requestOptions)
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
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        return data.result;
    });
}