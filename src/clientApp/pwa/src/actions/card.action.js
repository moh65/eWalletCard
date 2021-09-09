import {cardConstants} from '../constants/card.constants';
import {cardService} from "../services/card.service";


export const cardActions = {
    register,
    activate
};

function register(data) {
    return dispatch => {
        dispatch({type: cardConstants.FETCHING_REGISTER, isLoading: true});
        cardService.register(data)
            .then(
                res => {
                    dispatch({type: cardConstants.SET_REGISTER_RESULT, result: true});
                },
                error => {
                    dispatch({type: cardConstants.FAILED_REGISTER_FETCH, error});
                }
            );

    };
}


function activate(data) {
    return dispatch => {
        dispatch({type: cardConstants.FETCHING_ACTIVATE, isLoading: true});
        cardService.activate(data)
            .then(
                res => {
                    dispatch({type: cardConstants.SET_ACTIVATE_RESULT, result: true});
                },
                error => {
                    dispatch({type: cardConstants.FAILED_ACTIVATE_FETCH, error});
                }
            );

    };
}



