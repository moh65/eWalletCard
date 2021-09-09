import {clientConstants} from '../constants/client.constants';


const initialState = {error: "", states: [], info: {}, isLoading: false , data:{} , verifyResult : false , physicalResult:false , result:false  };

export function client(state = initialState, action) {
    switch (action.type) {
        case clientConstants.SET_CLIENT_STATES:
            return {
                ...state,
                isLoading: false,
                states: action.states,

            };
        case clientConstants.FETCHING_CLIENT_STATES:
            return {
                ...state,
                isLoading: true,
            };
        case clientConstants.FAILED_CLIENT_STATES_FETCH:
            return {
                ...state,
                error: action.error,
                isLoading: false
            };

        case clientConstants.SET_CLIENT_ADDRESS:
            return {
                ...state,
                isLoading: false,
                info: action.info,
            };
        case clientConstants.FETCHING_CLIENT_ADDRESS:
            return {
                ...state,
                isLoading: true,
            };
        case clientConstants.FAILED_CLIENT_ADDRESS_FETCH:
            return {
                ...state,
                error: action.error,
                isLoading: false
            };
        case clientConstants.SET_CLIENT_KYC:
            return {
                ...state,
                isKycLoading: false,
                result: action.result,
            };
        case clientConstants.FETCHING_CLIENT_KYC:
            return {
                ...state,
                isKycLoading: true,
            };
        case clientConstants.FAILED_CLIENT_KYC_FETCH:
            return {
                ...state,
                error: action.error,
                isKycLoading: false
            };

        case clientConstants.SET_KYC_DATA:
            return {
                ...state,
               data: action.data
            };

        case clientConstants.FETCHING_CLIENT_VERIFY:
            return {
                ...state,
                isVerifyLoading: true,
            };
        case clientConstants.FAILED_CLIENT_VERIFY_FETCH:
            return {
                ...state,
                error: action.error,
                isVerifyLoading: false
            };

        case clientConstants.SET_CLIENT_VERIFY:
            return {
                ...state,
                isVerifyLoading:false,
                verifyResult: action.verifyResult
            };
        case clientConstants.FETCHING_CLIENT_PHYSICAL:
            return {
                ...state,
                isPhysicalLoading: true,
                resultPhysical:false,
            };
        case clientConstants.FAILED_CLIENT_PHYSICAL_FETCH:
            return {
                ...state,
                error: action.error,
                isPhysicalLoading: false,
                resultPhysical:false,
            };

        case clientConstants.SET_CLIENT_PHYSICAL:
            return {
                ...state,
                isPhysicalLoading:false,
                error: null,
                physicalResult: action.physicalResult
            };
        default:
            return state
    }
}