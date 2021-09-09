import  * as config from './config';
import {authHeader} from "../helpers/authHeader";

export const clientService = {
    getStates,
    getAddress,
    kyc,
    verify,
    physical,
};


function physical(data) {
    const auth = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' , ...auth },
        body: JSON.stringify({nationalCode: data}),
    };
    return fetch(`${config.baseUrl}api/v1/client/physicalVerify`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}
function verify(data) {
    const auth = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' , ...auth },
        body: JSON.stringify(data),
    };
    return fetch(`${config.baseUrl}api/v1/client/verify`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}

function kyc(data) {
    const auth = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' , ...auth },
        body: JSON.stringify(data),
    };
    return fetch(`${config.baseUrl}api/v1/client/kyc`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}

function getStates(nationalCode) {
    const auth = authHeader();
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' , ...auth},
    };
    return fetch(`${config.baseUrl}api/v1/client/ClientStates?nationalCode=${nationalCode}`, requestOptions)
        .then(handleResponse)
        .then(result => {
            return result;
        });
}

function getAddress(nationalCode) {
    const auth = authHeader();
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' , ...auth },
    };
    return fetch(`${config.baseUrl}api/v1/client/Address?nationalCode=${nationalCode}`, requestOptions)
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
            const error = (data && data.message) || response.statusText || data.Message;
            return Promise.reject(error);
        }

        if(!data.result && !data.isSuccess ){
            const error = (data && data.message) || response.statusText || data.Message;
            return Promise.reject(error);
        }
        return data?.result;
    });
}