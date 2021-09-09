import { clientConstants } from '../constants/client.constants';
import { tclientService } from '../services/tclient.service';
import { history } from '../helpers/history';
import tokenExpire from '../helpers/tokenExpire';
export const clientActions = {
    list,
    upload,
    deleteRow,
    
};


function deleteRow(id) {
    return dispatch => {
        
        dispatch(loading(true));
        tclientService.deleteRow(id )
             .then(
                 client => { 
                     debugger
                     dispatch(list(0 , 20));
                     dispatch(loading(false));
                 },
                 error => {
                   
                     dispatch(failure(error));
                     dispatch(loading(false));
                 }
             );
     };

     function  loading(isloading) {
         return { type: clientConstants.CLIENT_REMOVING, isRemoveLoading : true}  
     }

   
 
     function success(client) { return { type: clientConstants.LIST_SUCCESS, client ,  isRemoveLoading : false}  }
     function failure(error) { return { type: clientConstants.LIST_FAILURE, error ,  isRemoveLoading : false}  }
}

function list(skip, take , mobile = "" , nationalCode = "") {
    return dispatch => {
        dispatch(searching(true));

        tclientService.list(skip, take ,nationalCode , mobile  )
            .then(
                client => { 
                    
                    dispatch(success(client));
                    dispatch(searching(false));
                },
                error => {
                    
                    if (error?.logout) {
                        dispatch(tokenExpire());
                    }
                    dispatch(failure(error));
                    dispatch(searching(false));
                }
            );
    };

    function  searching(isloading) {
        return { type: clientConstants.CLIENT_SEARCHING, isSearching : isloading}  
    }

    function success(client) { return { type: clientConstants.LIST_SUCCESS, client ,  isLoading : false}  }
    function failure(error) { return { type: clientConstants.LIST_FAILURE, error ,  isLoading : false}  }
}

function upload(file ) {
    return dispatch => {
         dispatch(loading(true));
 
         tclientService.upload(file)
             .then(
                 client => { 
                     dispatch(list(0 ,20));
                     dispatch(loading(false));
                 },
                 error => {
                    dispatch(list(0 , 20));
                     dispatch(failure(error));
                     dispatch(loading(false));
 
                 }
             );
     };

     function  loading(isloading) {
        return { type: clientConstants.CLIENT_FETCHING, isLoading : true}  
    }
     function failure(error) { return { type: clientConstants.LIST_FAILURE, error ,  isLoading : false}  }
}

