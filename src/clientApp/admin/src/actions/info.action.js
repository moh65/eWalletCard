import { infoConstants } from '../constants/info.constants';
import { infoService } from '../services/info.service';
import { history } from '../helpers/history';
import tokenExpire from '../helpers/tokenExpire';
export const infoActions = {
    loadData,    
};


function loadData(nationalCode) {
    return dispatch => {
        dispatch(searching(true));
        infoService.loadData(nationalCode)
            .then(
                client => { 
                    dispatch(searching(false));
                    dispatch(success(client));
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
        return { type: infoConstants.INFO_FETCHING, isLoading : isloading}  
    }

    function success(client) { return { type: infoConstants.INFO_SUCCESS, client ,  isLoading : false}  }
    function failure(error) { return { type: infoConstants.INFO_FAILURE, error ,  isLoading : false}  }
}


