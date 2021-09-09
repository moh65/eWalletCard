import { userConstants } from '../constants/user.constants';
import { userService } from '../services/user.service';

import { history } from '../helpers/history';
import tokenExpire from '../helpers/tokenExpire';

export const userActions = {
    login,
    logout,

};

function login(username, password) {
    return dispatch => {
       // dispatch(request({ username }));
       dispatch(loading(false));
        userService.login(username, password)
            .then(
                user => { 
                    dispatch(success(user , username));
                    history.push('/');
                },
                error => {

                    if (error?.logout) {
                        dispatch(tokenExpire());
                    }

                    dispatch(failure(error));
                }
            );
    };
    function  loading(isloading) {
        return { type: userConstants.USER_FETCHING, isLoading : true}  
    }
    function request(user) { return { type: userConstants.LOGIN_REQUEST, user ,username,  isLoading : true} }
    function success(user , username) { return { type: userConstants.LOGIN_SUCCESS, user ,  isLoading : false}  }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error ,  isLoading : false}  }
}

function logout() {
    userService.logout();
    return { type: userConstants.LOGOUT };
}

