import { logConstants } from '../constants/log.constants';
import { logService } from '../services/log.service';

import tokenExpire from '../helpers/tokenExpire';
export const logActions = {
    loadData,    
};


function loadData(skip, take , nationalCode) {

    return dispatch => {
        
        dispatch(searching(true));
        logService.loadData(skip, take , nationalCode)
            .then(
                logs => { 
                    dispatch(searching(false));
                    dispatch(success({ total: logs.total , data: logs.data.map((item) => JSON.parse(item.msg)) }));
                },
                error => {
                    if (error?.logout) {
                        dispatch(tokenExpire());
                    }
                    dispatch(searching(false));
                    dispatch(failure(error));
                   
                }
            );
    };

    function  searching(isloading) {
        return { type: logConstants.LOG_FETCHING, isLoading : isloading}  
    }

    function success(logs) { return { type: logConstants.LOG_SUCCESS, ...logs ,  isLoading : false}  }
    function failure(error) { return { type: logConstants.LOG_FAILURE, error ,  isLoading : false}  }
}


