import { Grid, GridColumn, GridDetailRow, GridToolbar  , Column } from '@progress/kendo-react-grid';
import '@progress/kendo-theme-default/dist/all.css';
import {  useSelector , useDispatch } from 'react-redux'
import {  useEffect , useState  } from 'react'
import  { clientActions } from '../../actions/tclient.action'
import ExcelUpload from './ExcelUpload'

import SearchBox from "./SearchBox";

const isLegalField = (props) => {
    const value = props.dataItem[props.field];
    return (
        <td style={{ color: value  ? "green" : "red" }}> 
            { value  && "مجاز" }
            { !value  && "غیر مجاز" }
        </td>
    )
}



const ClientList = () => {
    const [page , setPage] =  useState(1);
    const [skip , setskip] =  useState(0);
   
    const [take , settake] =  useState(20);    
    const dispatch = useDispatch();
    const data = useSelector(state =>   state.temporaryClient?.client );
    const isSearching =  useSelector(state => state.temporaryClient.isSearching)
    const isRemoveLoading = useSelector(state =>   state.temporaryClient?.isRemoveLoading );
    const removeId = useSelector(state =>   state.temporaryClient?.removeId );

    const onSearch = (mobile , nationalCode) => {
        dispatch(clientActions.list(0 , 20 , mobile ,nationalCode))
    }

    useEffect(() => {
        dispatch(clientActions.list(0 , 20));
    } , []);
    useEffect(() => {
       
        
    } , [data]);


    const deleteRow = (e , value) => {
        
       dispatch(clientActions.deleteRow(value))

       
    }
    
    const deleteColumn = (props) => {
        const value = props.dataItem[props.field];
        return (
            <td> 
                {removeId == value && isRemoveLoading &&  <button className="btn btn-sm btn-danger" >  لطفا کمی صبر کنید  </button> }
                {removeId != value &&  <button className="btn btn-sm btn-danger" onClick={(e) => deleteRow(e,value)}>  حذف  </button> }
               
            </td>
        )
    }


    const pageChange = (event) => {
        dispatch(clientActions.list(event.page.skip , event.page.take));
    }

    
        return (
            <div className="k-rtl">

                 <SearchBox onSearch={onSearch} isSearching={isSearching}/>

                  <Grid
                                style={{  direction: "rtl" , textAlign:'center'}}
                                pageable={true}
                                onPageChange={pageChange}
                                data={data?.data}
                                skip={skip}
                                take={take}
                                total={data?.total}
                            >
                                <GridToolbar>
                                    <ExcelUpload/>
                                </GridToolbar>
                                <GridColumn field="nationalCode" title="کدملی" />
                                <GridColumn field="isLegal" title="وضعیت"  cell={isLegalField} />
                                <GridColumn field="mobile" title="موبایل"  />
                                <GridColumn field="id" title="حذف"  cell={deleteColumn} width="240px" />
                            </Grid>
            </div>
        )
}

export default ClientList;