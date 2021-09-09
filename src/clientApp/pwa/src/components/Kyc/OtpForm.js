import {useForm} from "react-hook-form";
import ClipLoader from "react-spinners/ClipLoader";
const OtpForm = (props) => {
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

    return (
        <div className={""} style={{direction:"rtl" , textAlign:"right" , fontSize: 15 , background: "#014f86",color: "white",padding: 28 }}>

            <br />
            <p style={{textAlign:"center"}}> درخواست مفید کارت </p>
            <br/>
            <p className={"mofid-color"}> کد فعالسازی پیامک شده را وارد کنید </p>
            <form onSubmit={handleSubmit(d => props.submitHandler(d) )} >
                <div className="form-group">
                    <label className={"mofid-color"}> کد ارسالی </label>
                    <input type="text"  ref={register({ required: true})}  className="form-control mofid-input" name="otp" />
                    {errors.otp && <small style={{color:"red"}}> کد را وارد کنید </small>  }
                </div>
                <br />
                <br />
                { !props.isLoading && <button className={"btn btn-primary w-100"}> ثبت </button> }
                { props.isLoading && <button type={"button"} className={"btn btn-primary w-100"}>  <ClipLoader color={"#fff"} loading={true} size={20}/>  </button> }
                { props.error  && errorRender(props.error)}
                { props.errorClient && errorRender(props.errorClient) }
            </form>
        </div>
    )

}
const errorRender = (error) => {
    return(
        <small style={{color:'red'}}> {error} </small>
    )
}

export  default  OtpForm;