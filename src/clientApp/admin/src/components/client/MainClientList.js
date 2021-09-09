import { Grid, GridColumn, GridDetailRow, GridToolbar  , Column } from '@progress/kendo-react-grid';
import '@progress/kendo-theme-default/dist/all.css';
import {  useSelector , useDispatch } from 'react-redux'
import {  Link } from 'react-router-dom'
import {  useEffect , useState  } from 'react'
import {clientState } from "../../constants/clientState";
import  { clientActions } from '../../actions/client.action'


import SearchBox from "./SearchBox";



const StatesField = (props) => {
    const value = props.dataItem[props.field];
    return (
        <td> 
             { value  &&  value.length > 0 && clientState[value[value.length - 1]].name }       
        </td>
    )
}



const MainClientList = () => {
    const [page , setPage] =  useState(1);
    const [skip , setskip] =  useState(0);

    const [take , settake] =  useState(20);    
    const dispatch = useDispatch();
    const data = useSelector(state =>   state.client?.clients );
    const isSearching =  useSelector(state => state.client?.isSearching)
    useEffect(() => {
        dispatch(clientActions.list(0 , 20));
    } ,[]);
    useEffect(() => {
       
    } , [data]);

    const onSearch = (mobile , nationalCode) => {
        dispatch(clientActions.list(0 , 20 , mobile ,nationalCode))
    }

    const showInfo = () => {

    }
    const infoColumn = (props) => {
        const value = props.dataItem[props.field];
        return (
            <td> 
                 <Link className="btn btn-sm btn-info btn-sm" to={ "/info/"+ value }>  مشاهده اطلاعات  </Link> 
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
                                style={{direction: "rtl" , textAlign:'center'}}
                                pageable={true}
                                onPageChange={pageChange}
                                data={data?.data}
                                skip={skip}
                                take={take}
                                total={data?.total}
                            >
                                <GridToolbar>
                                </GridToolbar>
                                <GridColumn field="firstName" title="نام" />
                                <GridColumn field="lastName" title="نام خانوادگی" />
                                <GridColumn field="nationalCode" title="کدملی" />
                                <GridColumn field="states" title="وضعیت"  cell={StatesField} />
                                <GridColumn field="phoneNumber" title="موبایل"  />
                                <GridColumn field="nationalCode"  title="اطلاعات"  cell={infoColumn} />
                            </Grid>
            </div>
        )
}

export default MainClientList;