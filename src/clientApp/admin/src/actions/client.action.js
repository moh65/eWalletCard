import { mainclientConstants } from '../constants/mainclient.constants';
import { clientService } from '../services/client.service';
import { history } from '../helpers/history';
import tokenExpire from '../helpers/tokenExpire';
export const clientActions = {
    list,    
};


function list(skip, take , mobile = "" , nationalCode = "") {
    return dispatch => {
        dispatch(searching(true));

        clientService.list(skip, take ,nationalCode , mobile  )
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
        return { type: mainclientConstants.MAIN_CLIENT_SEARCHING, isSearching : isloading}  
    }

    function success(client) { return { type: mainclientConstants.MAIN_LIST_SUCCESS, client ,  isLoading : false}  }
    function failure(error) { return { type: mainclientConstants.MAIN_LIST_FAILURE, error ,  isLoading : false}  }
}


