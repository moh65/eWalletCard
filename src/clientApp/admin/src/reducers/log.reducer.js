import { logConstants } from '../constants/log.constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {   data: [] ,total : 0 , isLoading : false } : {};

export function log(state = initialState, action) {
    switch (action.type) {
    case logConstants.LOG_SUCCESS:
        return {
           data: action.data,
           total: action.total,
           isLoading: false
        };
    case logConstants.LOG_FAILURE:
        return {
          error: action.error,
          isLoading: false
        };
    case logConstants.LOG_FETCHING:
        return {
            isLoading: action.isLoading
        };
     
    default:
        return state
    }
}