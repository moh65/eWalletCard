import {clientState } from "../../constants/clientState";
const InfoContent = (props) => {
    const { data , isLoading , shouldShowError } = props;
    return (
       <div>
           {  isLoading  && <div className="alert">. ... درحال دریافت اطلاعات </div> }
           { !data && !isLoading && shouldShowError  && <div className="alert alert-danger"> اطلاعاتی یافت نشد </div> }
           { data && !isLoading  &&
            <div className="row text-right"> 
                <div className="col-md-4"> نام : {data.firstName} </div>
                <div className="col-md-4"> نام خانوادگی : {data.lastName} </div>
                <div className="col-md-4"> شماره تماس {data.phoneNumber} </div>
                <div className="col-md-4"> وضعیت : { data.states && data.states.length  && <span className="label" style={{backgroundColor:clientState[data.states[data.states.length - 1]].color, color:"white" , padding:5}}> {clientState[data.states[data.states.length - 1]].name} </span> } </div>
                <div className="col-md-4"> نام پدر : {data.fatherName}  </div>
                <div className="col-md-4"> مفید کارت : {data.mofidCard?.CardNumber} </div>
                <div className="col-md-4">  تاریخ تولد : {data.birthDate} </div>
                <div className="col-md-4">  سریال کارت ملی: {data.nationalCardSerial} </div>
                <div className="col-md-12"> آدرس :  {data.addresses.map((item , i) => <p key={i}> {item.state  && item.state + "-" }  {item.city && item.city + "-"}  {item.addressString && item.addressString }   {item.postalCode && " / " + item.postalCode  }  </p>)} </div>
           </div> }
       </div>
    )
}
export default InfoContent