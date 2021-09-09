
import {cardActions} from "../actions/card.action";
import {useDispatch, useSelector} from "react-redux";
import {deviceId} from "../helpers/device";
import {useEffect} from 'react';
import {useHistory} from 'react-router-dom'
import ClipLoader from "react-spinners/ClipLoader";
import {useForm} from "react-hook-form";

const VerifyActivePage = () => {
    const dispatch = useDispatch();
    const {phoneNumber, nationalCode} = useSelector(state => state.userLegal.data)
    const loading = useSelector(state => state.client.isVerifyLoading)
    const verifyResult = useSelector(state => state.client.verifyResult)
    const physicalResult = useSelector(state => state.client.physicalResult)
    const {resultVerify, isLoadingVerify, errorVerify} = useSelector(state => state.card)
    const submitHandler = (data) => {
        
       
        dispatch(cardActions.activate({...data , phoneNumber ,nationalCode }));
    }   

    const { register, handleSubmit , errors } = useForm({
        // resolver: yupResolver(schema),
        resolver: undefined,
        defaultValues: {

        },
        context: undefined,
        reValidateMode: 'onChange',
        shouldFocusError: true,
        shouldUnregister: true,
        mode: 'onSubmit',
    });

    const history = useHistory();
    useEffect(() => {
        if(resultVerify) {
            history.push("/preview");
        }
    },resultVerify);




    return (
        <>
            <div className={"container d-flex justify-content-center align-items-center"} >
                <div className={""} style={{direction:"rtl" , textAlign:"right" , fontSize: 15 , background: "#014f86",color: "white",padding: 28 }}>

                    <br />
                    <p style={{textAlign:"center"}}> فعالسازی مفید کارت </p>
                    <br/>
                    <p className={"mofid-color"}> شماره کارت </p>
                    <form onSubmit={handleSubmit(d => submitHandler(d) )} >
                        <div className="form-group">

                            <input type="text"  ref={register({ required: true})}  className="form-control mofid-input" name="pan" />
                            {errors.otp && <small style={{color:"red"}}> شماره کارت را وارد کنید </small>  }
                        </div>
                        <br />
                        <br />
                        { !isLoadingVerify && <button className={"btn btn-primary w-100"}> فعالسازی </button> }
                        { isLoadingVerify && <button type={"button"} className={"btn btn-primary w-100"}>  <ClipLoader color={"#fff"} loading={true} size={20}/>  </button> }
                    </form>

                    {errorVerify && renderError(errorVerify)}

                </div>
            </div>

            <br />

        </>
    );
}

const renderError = (error) => {
    return (<small style={{color:"red"}}>{error}</small>);
}

export default VerifyActivePage;