import { Link } from 'react-router-dom'
import { AuthInfo  } from './AuthInfo'
const Header  = () => {
    return (
        <div style={{direction: "rtl"}}>
            <nav className="navbar navbar-expand-lg navbar-light bg-light ">
                <div className="container">
                    <a className="navbar-brand" href="#"> مفید کارت </a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>

                    <div className="collapse navbar-collapse justifyContent" id="navbarSupportedContent">
                        <ul className="navbar-nav mr-auto">
                       
                        <li className="nav-item">
                            <Link to="/" className="nav-link" > وایت لیست </Link>
                        </li>
                        <li className="nav-item">
                            <Link to="/client" className="nav-link" > مشتریان </Link>
                        </li>

                     
                        <li className="nav-item">
                            <Link to="/info" className="nav-link" > جست و جوی مشتری </Link>
                        </li>

                        <li className="nav-item">
                            <Link to="/setting" className="nav-link" > تنظیمات سیستم </Link>
                        </li>

                        <li className="nav-item active">
                            <Link to="/logs" className="nav-link" > لاگ api </Link>
                        </li>
                    </ul>

                    <div className="self-align-left">
                        <AuthInfo />
                    </div>
                </div>
            </div>
            </nav>
        </div>
    )
}


export default Header;