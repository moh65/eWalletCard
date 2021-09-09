import { useState } from 'react'
import {  useSelector } from 'react-redux'
const SearchBox = (props) => {
    const [search , setSearch] = useState({ mobile: "" , nationalCode : "" })
    const isLoading = props.isSearching;
    const onSearch = props.onSearch; 
    return (
        <>
            <div className="row" style={{marginBottom:4}}>
                <div className="col-md-4"><input className="form-control" placeholder="شماره موبایل" name="mobile" value={search.mobile} onChange={e => setSearch({ ...search , mobile: e.target.value })} /></div>
                <div className="col-md-4"><input className="form-control" placeholder="کد ملی" name="nationalCode" value={search.nationalCode} onChange={e => setSearch({ ...search , nationalCode: e.target.value })}  /></div>
                <div className="col-md-4">
                    {isLoading && <button className="btn btn-secondary" > درحال جست و جو </button>}
                    {!isLoading &&  <button className="btn btn-secondary" onClick={e => onSearch(search.mobile ,search.nationalCode)}> جست و جو </button>}
                </div>
                
            </div>
            <hr />
        </>
    )
}

export default SearchBox;