import { nationalCodeConstants } from '../constants/nationalCode.constants';
import {nationalCodeService} from "../services/nationalCode.service";




export const nationalCodeActions = {
    set,
};

function set(data) {
    return dispatch => {
        dispatch({ type: nationalCodeConstants.FETCHING_NATIONAL_CODE , isLoading: true });
        nationalCodeService.checkNationalCodeInTemporary(data.nationalCode)
            .then(
                res => {

                    if(res == true){
                        dispatch({ type: nationalCodeConstants.SET_CURRENT_USER , data });

                        sessionStorage.setItem("userDetails" , JSON.stringify(data));
                    }else{
                        dispatch({ type: nationalCodeConstants.NATIONAL_CODE_ILLEGAL , error: "کد ملی مجاز نیست" });
                    }

                },
                error => {

                    try{ 
                    dispatch({ type: nationalCodeConstants.NATIONAL_CODE_ILLEGAL , error});
                    }catch(e){

                    }
                }
            );

    };
}


