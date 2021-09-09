import OtpForm from "../components/Kyc/OtpForm";
import {clientActions} from "../actions/client.action";
import {useDispatch, useSelector} from "react-redux";
import {deviceId} from "../helpers/device";
import {useEffect} from 'react';
import {useHistory} from 'react-router-dom'

const OtpKycPage = () => {
    const dispatch = useDispatch();
    const {phoneNumber, nationalCode} = useSelector(state => state.client.data)
    const loading = useSelector(state => state.client.isVerifyLoading)
    const verifyResult = useSelector(state => state.client.verifyResult)
    const physicalResult = useSelector(state => state.client.physicalResult)
    const {result, isLoading, error} = useSelector(state => state.card)


    const errors = useSelector(state => state.client.error)
    const submitHandler = (data) => {
        dispatch(clientActions.verify({
            ...data,
            deviceId: deviceId(),
            phoneNumber: phoneNumber,
            nationalCode: nationalCode
        }));
    }
    const history = useHistory();
    useEffect(() => {

        if (verifyResult && result && physicalResult) {
            history.push("/preview")
        }
    }, [verifyResult, result, physicalResult]);


    useEffect(() => {
        if (!phoneNumber) {
            history.push("/kyc")
        }
    }, [phoneNumber])

    return (
        <>
            <div className={"container d-flex justify-content-center align-items-center"} >


                <OtpForm submitHandler={submitHandler} isLoading={(loading || isLoading)} error={error} errorClient={errors}/>
            </div>
            <br />

        </>
    );
}
export default OtpKycPage;