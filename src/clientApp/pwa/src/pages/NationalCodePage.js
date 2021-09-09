import "../content/css/NationalCode.css"
import { useForm  } from 'react-hook-form';
import {nationalCodeActions} from "../actions/nationalCode.action";
import  { useSelector  , useDispatch } from "react-redux";
import ClipLoader from "react-spinners/ClipLoader";
import { useHistory , useParams , useLocation } from "react-router-dom";
import { useEffect } from 'react'
import logo from '../logo.png'
const NationalCodePage = () => {
    const history = useHistory();
    const dispatch = useDispatch();
    const { register, handleSubmit , errors } = useForm({
        // resolver: yupResolver(schema),
        resolver: undefined,
        context: undefined,
        reValidateMode: 'onChange',
        shouldFocusError: true,
        shouldUnregister: true,
        mode: 'onSubmit',
    });

    const location = useLocation();
    const afterLogin = new URLSearchParams(location.search).get("afterLogin")


    const submitLogin = (data) => {
        dispatch(nationalCodeActions.set(data));
    }

    const _data = useSelector(state => state.userLegal?.data);
    const loading = useSelector(state => state.userLegal?.isLoading);
    const error = useSelector(state => state.userLegal?.error);

    useEffect(() => {
        if(_data?.nationalCode) {

            if(afterLogin){
                history.push(afterLogin);
            }else {
                history.push("/preview");
            }
        }
    }, [_data])

    return (
        <div className="container center-center" style={{direction: "rtl", textAlign: 'right'}}>
            <div className="card" style={{width:"100%"}}>
                <div className="card-body">
                    <form onSubmit={handleSubmit(d =>  submitLogin(d) )}>

                        <div className="d-flex justify-content-center">
                        <img src={logo} width={100} />
                        </div>
                        <br />
                        <div className="form-group">
                            <label> کد ملی </label>
                            <input type="text"  ref={register({ required: true})}  className="form-control" name="nationalCode"/>
                            {errors.nationalCode && <small style={{color:"red"}}> کد ملی را وارد کنید </small>  }
                        </div>

                        <br/>

                        <div className="form-group">
                            <label> شماره تلفن </label>
                            <input type="text"  ref={register({ required: true})}  className="form-control" name="phoneNumber"/>
                            {errors.phoneNumber && <small style={{color:"red"}}> شماره را وارد کنید </small>  }
                        </div>
                        <br />

                        { loading && <button className="btn btn-block btn-primary w-100" disabled={true}>   <ClipLoader color={"#fff"} loading={true} size={20}/> </button> }

                        { !loading && <button className="btn btn-block btn-primary w-100"> ذخیره</button> }
                        <br />
                        { error &&  <p style={{marginTop:7}} className="alert alert-danger"> {error} </p> }
                    </form>
                </div>
            </div>
        </div>
    )
}
export default NationalCodePage;