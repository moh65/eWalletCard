import { useEffect } from 'react';
import {useDispatch, useSelector} from "react-redux";
import { tbsActions } from '../../actions/tbs.action';
import ClipLoader from "react-spinners/ClipLoader";
import {useHistory} from 'react-router-dom';
const Tbs = () => {
    const history = useHistory();
    const dispatch = useDispatch();

    const  nationalCode  = useSelector(state => state.userLegal.data?.nationalCode);
    const loading = useSelector(state => state.tbs.isLoading);
    useEffect(()=> {
        if(nationalCode)
            dispatch(tbsActions.getRemain(nationalCode));
    },[]);


    useEffect(()=> {
      if(!nationalCode){
          history.push("/nationalcode");
      }
    },[nationalCode]);



    const amount = useSelector(state => state.tbs?.amount);
    return (
        <>
            <div className="col-12">
                <p style={{textAlign:"center"}}>  نزد کارگزاری  </p>
                <p style={{textAlign:"center"}}>  { loading &&  <ClipLoader color={"#fff"} loading={true} size={20}/> } { !loading && " ریال "  + amount?.toLocaleString()  }  </p>
            </div>
            <div className="col-6">
                <button className="btn btn-primary"  style={{fontSize:10}}> افزایش موجودی  </button>
            </div>
            <div className="col-6">
                <button className="btn btn-primary"  style={{fontSize:10}}> برداشت موجودی  </button>
            </div>


        </>
)
}
export default Tbs;