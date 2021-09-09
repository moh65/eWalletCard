import { infoConstants } from '../constants/info.constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user  , isLoading : false } : {};

export function info(state = initialState, action) {
    switch (action.type) {
    case infoConstants.INFO_SUCCESS:
        return {
           data: action.client,
           isLoading: false
        };
    case infoConstants.INFO_FAILURE:
        return {
          error: action.error,
          isLoading: false
        };
    case infoConstants.INFO_FETCHING:
        return {
            isLoading: action.isLoading
        };
     
    default:
        return state
    }
}