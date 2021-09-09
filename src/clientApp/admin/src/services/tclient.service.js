import  * as config from './config';
import {authHeader} from '../helpers/authHeader'

export const tclientService = {
    list,
    upload,
    deleteRow,
};

function deleteRow (id) {
    var header = authHeader();
    const requestOptions = {
        method: 'POST',
        headers: { ...header , "Content-Type" : "application/json" },
        body : JSON.stringify({ id: id })
    };

    return fetch(`${config.baseUrl}api/v1/TemporaryClient/Delete` , requestOptions)
    .then(res => res.json())
    .then(res => {
        
        if(!res.result) {
            const error = (res && res.message) || res.message  || res.Message;
            return Promise.reject(error);
        }

        return res.result;
    });
}

function list(skip , take , nationalCode = "", mobile = "") {
    var header = authHeader();
    const requestOptions = {
        method: 'GET',
        headers: { ...header },
    };

    return fetch(`${config.baseUrl}api/v1/TemporaryClient/list?skip=${skip}&take=${take}&nationalCode=${nationalCode}&Mobile=${mobile}` , requestOptions)
        .then(handleResponse)
        .then(clients => {
            return clients;
        });
}

function upload(file) {
    
    var header = authHeader();
    const _data = new FormData();
    _data.append("WhiteList" , file);
    const requestOptions = {
        method: 'POST',
        headers: { ...header  },
        body: _data,
    };

    return fetch(`${config.baseUrl}api/v1/TemporaryClient/importExcel`, requestOptions)
        .then(handleResponse)
        .then(clients => {
            return clients;
        });
}


function handleResponse(response) {
    return response.json().then(data => {
        debugger
      
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