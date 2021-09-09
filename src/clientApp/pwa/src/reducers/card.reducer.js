import { cardConstants } from "../constants/card.constants";


const initialState =  { error: "" ,result: false , isLoading : false } ;

export function card(state = initialState, action) {
    switch (action.type) {
    case cardConstants.SET_REGISTER_RESULT:
        return {
            isLoading: false,
            result: action.result,
        };
    case cardConstants.FETCHING_REGISTER:
        return {
            isLoading: true,
        };
    case cardConstants.FAILED_REGISTER_FETCH:
        return {
            error: action.error,
            isLoading: false
        };


    
        case cardConstants.SET_ACTIVATE_RESULT:
            return {
                isLoadingActivate: false,
                resultActivate: action.result,
            };
        case cardConstants.FETCHING_ACTIVATE:
            return {
                isLoadingActivate: true,
            };
        case cardConstants.FAILED_ACTIVATE_FETCH:
            return {
                errorActivate: action.error,
                isLoadingActivate: false
            };


            case cardConstants.SET_VERIFIED_ACTIVATE_RESULT:
            return {
                isLoadingVerified: false,
                resultVerified: action.result,
            };
        case cardConstants.FETCHING_VERIFIED_ACTIVATE:
            return {
                isLoadingVerified: true,
            };
        case cardConstants.FAILED_VERIFIED_ACTIVATE_FETCH:
            return {
                errorVerified: action.error,
                isLoadingVerified: false
            };
    default:
        return state
    }



}