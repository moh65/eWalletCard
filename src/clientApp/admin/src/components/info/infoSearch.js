const InfoSearch = (props) => {
    return (
        <>
        <div className="row">
            <div className="col-md-4">  <input className="form-control" name="nationalCode" value={props.nationalCode} placeholder="کد ملی مثلا 4900729671" onChange={e => props.onChange(e.target.value)} /> </div>                
            <div className="col-md-2"> 
                { props.isLoading &&  <button className="btn btn-primary"> لطفا کمی صبر کنید </button> }
                { !props.isLoading &&  <button className="btn btn-primary" onClick={props.onClick}> جست و جو </button> }
             </div>                
           
        </div>
        <br/>
             <hr />
             <br/>
        </>
    )
}
export default InfoSearch