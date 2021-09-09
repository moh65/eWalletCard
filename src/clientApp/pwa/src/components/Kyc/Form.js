import { useForm  } from 'react-hook-form';
import ClipLoader from "react-spinners/ClipLoader";
import DatePicker from 'react-datepicker2';
import moment from "moment-jalaali";
 import { useState } from 'react';
const Form = (props) => {

    const { register, handleSubmit , errors } = useForm({
        // resolver: yupResolver(schema),
        resolver: undefined,
        defaultValues: {
            nationalCode:props.info.nationalCode,
            phoneNumber:props.info.phoneNumber,
            addressString:props.info?.addressCity + " " + props.info?.address,
            postalCode: props.info?.postalCode,
            birthDate: props.info?.birthDate
        },
        context: undefined,
        reValidateMode: 'onChange',
        shouldFocusError: true,
        shouldUnregister: true,
        mode: 'onSubmit',
    });



    const [birthDate , setBirthDate] = useState(props.info.birthDate ?? "1399/01/01");
    
   const submitDate = ({value}) => {
        console.log(value);
        setBirthDate(value)
   }
    return (
        <div className={"container"} style={{direction:"rtl" , textAlign:"right" , fontSize: 17 }}>

            <br />
            <p style={{textAlign:"center"}}> درخواست مفید کارت </p>

            <form onSubmit={handleSubmit(d =>  { if(birthDate == "" || birthDate == null) { return false; }  props.submitHandler({...d , NationalCardSerial: props.info.nationalCode , birthDate});  })}>
                <div className="form-group">
                    <label className={"mofid-color"}> موبایل </label>
                    <input type="text"  ref={register({ required: true})}  className="form-control mofid-input" name="phoneNumber" />
                    {errors.phoneNumber && <small style={{color:"red"}}> موبایل را وارد کنید </small>  }
                </div>
                <br />

                <div className="form-group">
                    <label className={"mofid-color"}> کد ملی </label>
                    <input type="text"  ref={register({ required: true })}  className="form-control mofid-input" name="nationalCode"/>
                    {errors.nationalCode && <small style={{color:"red"}}> کد ملی را وارد کنید </small>  }
                </div>
                <br />
                <div className="form-group">
                    <label className={"mofid-color"}> تاریخ تولد </label>
                    
                                        {/* <input type="text"  ref={register({ required: true })}  className="form-control mofid-input" name="birthDate"/> */}
                    <DatePicker  
                    isGregorian={false}
                    timePicker={false}
                    className={"form-control mofid-input direction-rtl"}

                        value={moment(birthDate , 'jYYYY/jM/jD')}
                        onChange={value => {setBirthDate(value.format('jYYYY/jM/jD'))}}
                    />
                    { birthDate == "" || birthDate == null && <small style={{color:"red"}}>  تاریخ تولد را وارد کنید </small>  }
                </div>
                <br />
                <div className="form-group">
                    <label className={"mofid-color"}> آدرس پستی </label>
                    <textarea type="text"  ref={register({ required: true})}  className="form-control mofid-input" name="addressString">
                    </textarea>
                    {errors.addressString && <small style={{color:"red"}}> آدرس پستی را وارد کنید </small>  }
                </div>
                <br />
                <div className="form-group">
                    <label className={"mofid-color"}> کد پستی </label>
                    <input type="text"  ref={register({ required: true})}  className="form-control mofid-input" name="postalCode"/>
                    {errors.postalCode && <small style={{color:"red"}}>  کد پستی را وارد کنید </small>  }
                </div>
                <br />
                { !props.isLoading && <button className={"btn btn-primary w-100"}> تایید </button> }
                { props.isLoading &&<button type={"button"} className={"btn btn-primary w-100"}>  <ClipLoader color={"#fff"} loading={true} size={20}/>  </button> }

            </form>
        </div>
    )
}
export  default Form;