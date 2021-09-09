import  {nationalCodeConstants} from "../constants/nationalCode.constants";

let userDetails = sessionStorage.getItem("userDetails") ? JSON.parse( sessionStorage.getItem("userDetails")) : {};
const initialState =  { data: userDetails, error: "" , isLoading : false } ;

export function userLegal(state = initialState, action) {
    switch (action.type) {
        case nationalCodeConstants.SET_CURRENT_USER:
            return {
                data: action.data,
                isLoading: false
            };
        case nationalCodeConstants.FETCHING_NATIONAL_CODE:
            return {
                isLoading: action.isLoading
            };
        case nationalCodeConstants.NATIONAL_CODE_ILLEGAL:
            return {
                error: action.error
            };
        default:
            return state
    }
}