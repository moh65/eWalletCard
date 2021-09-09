import {tbsConstants} from '../constants/tbs.constants';
import {tbsService} from "../services/tbs.service";


export const tbsActions = {
    getRemain,
};

function getRemain(nationalCode) {
    return dispatch => {
        dispatch({type: tbsConstants.FETCHING_TBS_AMOUNT, isLoading: true});
        tbsService.getAmount(nationalCode)
            .then(
                res => {
                    dispatch({type: tbsConstants.SET_TBS_AMOUNT, amount: res});
                },
                error => {

                    dispatch({type: tbsConstants.FAILED_TBS_AMOUNT_FETCH, error});
                }
            );

    };
}


