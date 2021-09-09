import { tbsConstants } from '../constants/tbs.constants';



const initialState =  { error: "" ,amount: 0 , isLoading : false } ;

export function tbs(state = initialState, action) {
    switch (action.type) {
    case tbsConstants.SET_TBS_AMOUNT:
        return {
            isLoading: false,
            amount: action.amount,
        };
    case tbsConstants.FETCHING_TBS_AMOUNT:
        return {
            isLoading: true,
        };
    case tbsConstants.FAILED_TBS_AMOUNT_FETCH:
        return {
            error: action.error,
            isLoading: false
        };
    default:
        return state
    }
}