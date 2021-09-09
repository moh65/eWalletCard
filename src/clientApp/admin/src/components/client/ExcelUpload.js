import { useDispatch , useSelector } from 'react-redux'
import { clientActions } from '../../actions/tclient.action'
const ExcelUpload = () => {

    const dispatch = useDispatch();
    
    const isLoading = useSelector(state => state.temporaryClient.isLoading)
    const showFiles = () => {
        document.getElementById("fileUpload").click();
    }
    const onChange = (e) => {
        dispatch(clientActions.upload(e.target.files[0]));
        e.target.value = "";
    }
    return(
                    <div>
                        { isLoading && <button className="btn btn-primary btn-block" >    درحال اپلود ...   </button> }
                        { !isLoading && <button className="btn btn-primary btn-block" onClick={showFiles}>    انتخاب فایل excel   </button> }
                        
                        <input  type="file"  id="fileUpload" style={{"display":"none"}} onChange={onChange} />
                    </div>
    )
}
export default ExcelUpload;