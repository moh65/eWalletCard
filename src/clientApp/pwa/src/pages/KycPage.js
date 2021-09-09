
import {clientActions} from "../actions/client.action";
import {clientConstants} from "../constants/client.constants";
import  { useSelector  , useDispatch } from "react-redux";
import ClipLoader from "react-spinners/ClipLoader";
import { useHistory } from "react-router-dom";
import { useEffect } from 'react'
import {deviceId} from "../helpers/device";
import Form from "../components/Kyc/Form";


const KycPage = () => {
    const history = useHistory();

    const { nationalCode , phoneNumber } = useSelector(state => state.userLegal.data);
    const isLoading = useSelector(state => state.client.isLoading);
    const info = useSelector(state => state.client.info) ?? {};
    const otp = useSelector(state => state.client.result) ?? false;
    const isKycLoading = useSelector(state => state.client.isKycLoading);
    const dispatch = useDispatch();

    const submitHandler = (data)  => {
        dispatch({ type:clientConstants.SET_KYC_DATA , data :data   })
        dispatch(clientActions.kyc({...data , deviceId: deviceId() }));
    }

    useEffect(() => {
        if(otp == true){
            history.push("/kycOtp");
        }
    } , [otp])


    useEffect(() => {
        if(nationalCode)
            dispatch(clientActions.getAddress(nationalCode));
    } , []);

    useEffect(() => {
        if(!nationalCode){
            history.push(`/nationalCode?afterLogin=/kyc`)
        }
    }, [nationalCode]);

    return (

        <>

            {isLoading && <div className={"container center-center"}>

                <ClipLoader color={"#000"} loading={true} size={20}/>

            </div>}

            {!isLoading && <div className={"container center-center"}>
                <Form info={{ phoneNumber: phoneNumber , nationalCode: nationalCode , ...info }} submitHandler={submitHandler} isLoading={isKycLoading} />
            </div>}


        </>

    )
}

export default  KycPage;