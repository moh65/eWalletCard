import { mainclientConstants } from '../constants/mainclient.constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {  isLoading : false , clients : {} ,removeId : 0, isRemoveLoading: false  , isSearching: false} : {};

export function client(state = initialState, action) {
    switch (action.type) {

    case mainclientConstants.MAIN_LIST_SUCCESS:
            return {
            
            clients: action.client,
            isLoading: action.isLoading
            };

    case mainclientConstants.MAIN_DELETE_SUCCESS:
                return {
                    deleted:true,
                    isLoading: action.isLoading
                };
     case mainclientConstants.MAIN_CLIENT_FETCHING:
         return {
             ...state,
             isLoading: action.isLoading
         };
     case mainclientConstants.MAIN_CLIENT_REMOVING:
        return {
            ...state,
            isRemoveLoading: action.isRemoveLoading
        };

        case mainclientConstants.MAIN_CLIENT_SEARCHING:
            return {
                ...state,
                isSearching: action.isSearching
            };
    
    default:
        return state
    }
}