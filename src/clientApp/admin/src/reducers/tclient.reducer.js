import { clientConstants } from '../constants/client.constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {  isLoading : false , client : {} ,removeId : 0, isRemoveLoading: false  , isSearching: false} : {};

export function temporaryClient(state = initialState, action) {
    switch (action.type) {
    case clientConstants.UPLOAD_EXCEL_TEMPORARY:
        return {
        
        filename: action.filename,
        isLoading: action.isLoading
        };
   
    case clientConstants.LIST_SUCCESS:
            return {
            
            client: action.client,
            isLoading: action.isLoading
            };

    case clientConstants.DELETE_SUCCESS:
                return {
                
                    deleted:true,
                isLoading: action.isLoading
                };
     case clientConstants.CLIENT_FETCHING:
         return {
             ...state,
             isLoading: action.isLoading
         };
     case clientConstants.CLIENT_REMOVING:
        return {
            ...state,
            isRemoveLoading: action.isRemoveLoading
        };

        case clientConstants.CLIENT_SEARCHING:
            return {
                ...state,
                isSearching: action.isSearching
            };
    
    default:
        return state
    }
}