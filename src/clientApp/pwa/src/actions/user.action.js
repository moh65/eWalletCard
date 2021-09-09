import { userConstants } from '../constants/user.constants';
import { userService } from '../services/user.service';



export const userActions = {
    login,
    logout,
};

function login(username, password) {
    return dispatch => {
       dispatch(loading(true));

        userService.login({username, password})
            .then(
                user => { 
                    dispatch(success(user , username));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function  loading(isloading) {
        return { type: userConstants.USER_FETCHING, isLoading : true}  
    }
    function success(user , username) { return { type: userConstants.LOGIN_SUCCESS, user ,  isLoading : false}  }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error ,  isLoading : false}  }
}

function logout() {
    userService.logout();
    return { type: userConstants.LOGOUT };
}

