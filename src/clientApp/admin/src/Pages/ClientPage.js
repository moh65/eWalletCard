import '../css/client.css'
// import ExcelUpload from '../components/client/ExcelUpload'
 import MainClientList from '../components/client/MainClientList'
 
const ClientPage = () => {
    return (  
        <div>
           
            <div className="container text-right full-body">
                <div className="card"> 
                    <div className="card-header">
                        مشتریان     
                    </div>

                    <div className="cardbody">
                        <div className="p-2">
                            {/* <ExcelUpload /> */}
                            <MainClientList />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
  }
  
  export default ClientPage;
  
  