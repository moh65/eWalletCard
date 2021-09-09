import '../css/client.css'
// import ExcelUpload from '../components/client/ExcelUpload'
 import ClientList from '../components/client/ClientList'
 
const TemporaryClientPage = () => {
    return (  
        <div>
            
            <div className="container text-right  full-body">
                <div className="card"> 
                    <div className="card-header">
                        کاربران     
                    </div>

                    <div className="cardbody">
                        <div className="p-2">
                            {/* <ExcelUpload /> */}
                            <ClientList />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
  }
  
  export default TemporaryClientPage;
  
  