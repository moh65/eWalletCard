import { useState , useEffect } from 'react'
import { useParams } from "react-router-dom";
import { infoActions } from '../actions/info.action'
import { useDispatch , useSelector } from "react-redux";
import  InfoSearch  from "../components/info/infoSearch";
import  InfoContent  from "../components/info/InfoContent";
const InfoPage = () => {

    const { id } = useParams();
    const [nationalCode , setNationalCode]= useState(id);
    const [shouldShowError , setShouldShowError]= useState(false);
    const dispatch = useDispatch();
    const isLoading =  useSelector(state => state.info.isLoading)
    const data =  useSelector(state => state.info.data)
    useEffect(() => {
      if(nationalCode){
        onClick();
      }
    } ,[])

    const onClick =()=> {
        
        dispatch(infoActions.loadData(nationalCode))
        setShouldShowError(true);
    }

    return (
        <div className="container  full-body" style={{marginTop:5 , direction:"rtl"}} >
            <div className="card">
                <div className="card-header">  اطلاعات کد ملی {nationalCode} </div>
                <div className="card-body">
                    <InfoSearch nationalCode={nationalCode} onChange={setNationalCode} isLoading={isLoading} onClick={onClick} />
                    <InfoContent isLoading={isLoading} data={data} shouldShowError={shouldShowError}  />
                </div>
            </div>
        </div>
    )
}
export default InfoPage;