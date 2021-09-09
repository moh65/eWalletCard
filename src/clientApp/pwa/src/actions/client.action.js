import {clientConstants} from '../constants/client.constants';
import {clientService} from "../services/client.service";
import {cardActions} from "./card.action";


export const clientActions = {
    getStates,
    getAddress,
    kyc,
    verify,
    physical
};


function physical(data) {
    return dispatch => {
        dispatch({type: clientConstants.FETCHING_CLIENT_PHYSICAL });
        clientService.physical(data.nationalCode)
            .then(
                res => {
                    dispatch({type: clientConstants.SET_CLIENT_PHYSICAL, physicalResult: true});
                    dispatch(cardActions.register({nationalCode: data.nationalCode , phoneNumber: data.phoneNumber}));
                },
                error => {

                    dispatch({type: clientConstants.FAILED_CLIENT_PHYSICAL_FETCH, error});
                }
            );

    };
}

function  verify(data) {
    return dispatch => {
        dispatch({type: clientConstants.FETCHING_CLIENT_VERIFY });
        clientService.verify(data)
            .then(
                res => {

                    dispatch({type: clientConstants.SET_CLIENT_VERIFY, verifyResult: true});

                    dispatch(physical(data))
                },
                error => {

                    dispatch({type: clientConstants.FAILED_CLIENT_VERIFY_FETCH, error});
                }
            );

    };
}

function  kyc(data) {
    return dispatch => {
        dispatch({type: clientConstants.FETCHING_CLIENT_KYC });
        clientService.kyc(data)
            .then(
                res => {
                    dispatch({type: clientConstants.SET_CLIENT_KYC, result: true});
                },
                error => {

                    dispatch({type: clientConstants.FAILED_CLIENT_KYC_FETCH, error});
                }
            );

    };
}

function getStates(nationalCode) {
    return dispatch => {
        dispatch({type: clientConstants.FETCHING_CLIENT_STATES, isLoading: true});
        clientService.getStates(nationalCode)
            .then(
                res => {
                    dispatch({type: clientConstants.SET_CLIENT_STATES, states: res});
                },
                error => {

                    dispatch({type: clientConstants.FAILED_CLIENT_STATES_FETCH, error});
                }
            );

    };
}

function getAddress(nationalCode) {
    return dispatch => {
        dispatch({type: clientConstants.FETCHING_CLIENT_ADDRESS, isLoading: true});
        clientService.getAddress(nationalCode)
            .then(
                res => {
                    dispatch({type: clientConstants.SET_CLIENT_ADDRESS, info: res});
                },
                error => {

                    dispatch({type: clientConstants.FAILED_CLIENT_ADDRESS_FETCH, error});
                }
            );

    };
}



