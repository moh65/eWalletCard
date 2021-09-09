import { userConstants } from '../constants/user.constants';

let user = JSON.parse(localStorage.getItem('user'));
let username = localStorage.getItem('username');
const initialState = user ? { loggedIn: true, user ,username  : username , isLoading : false } : {};

export function authentication(state = initialState, action) {
    switch (action.type) {
    case userConstants.LOGIN_REQUEST:
        return {
        loggingIn: true,
        user: action.user,
        isLoading: action.isLoading
        };
    case userConstants.LOGIN_SUCCESS:
        return {
        loggedIn: true,
        user: action.user,
        username: action.username,
        isLoading: action.isLoading
        };
    case userConstants.LOGIN_FAILURE:
        return {
            isLoading: action.isLoading
        };
        case userConstants.USER_FETCHING:
            return {
                ...state,
                isLoading: action.isLoading
            };
    case userConstants.LOGOUT:
        return {
            isLoading: action.isLoading
        };
    default:
        return state
    }
}