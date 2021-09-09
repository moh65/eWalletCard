import '../css/login.css'

import logo from '../images/logo.png'
import { useForm  } from 'react-hook-form';
import { useSelector , useDispatch  } from "react-redux"
import { userActions } from "../actions/user.action"
import { useHistory } from "react-router-dom"
import { useEffect , useState } from 'react';

const LoginPage = () => {
    
      const { register, handleSubmit , errors } = useForm({
        // resolver: yupResolver(schema),
        resolver: undefined,
        context: undefined,
        reValidateMode: 'onChange',
        shouldFocusError: true,
        shouldUnregister: true,
        mode: 'onSubmit',
      });
      const history = useHistory();

     
      const [ loginClicked , setloginClicked ] = useState(false);
      const user = useSelector(state =>  state.authentication?.user );
      const isloading = useSelector(state =>  state.authentication?.isLoading );



      useEffect( function () {
            if(user && loginClicked){
            history.push("/");
            }
        },[user]);

      const dispatch = useDispatch();
      const submitLogin = (data) => {
        setloginClicked(true);
        dispatch(userActions.login(data))
      }

    return (
            <div className="conatiners login">
             <div className="container">
                 <div className="card login">
                     <div className="card-header">
                         <h5> ورود به پنل </h5>
                     </div >
                     <div className="card-body">
                         <img src={logo} className="rounded-circle" width="100" />
                         <form className="form login" onSubmit={handleSubmit(d =>  submitLogin(d) )}>
                             <div className="form-group">
                                 <label> نام کاربری </label>
                                 <input  className="form-control"  ref={register({  required: true})}   name="username"  />
                                 {errors.username && <small className="text-danger"> نام کاربری را وارد کنید </small>}
                             </div>      
                             <div className="form-group">
                                 <label> رمز عبور </label>
                                 <input  className="form-control"  ref={register({  required: true })}   name="password" type="password"  />
                                 {errors.password && <small className="text-danger"> رمز عبور را وارد کنید </small>}
                             </div>   
                             <button className="btn btn-primary btn-block mt-2 w-100">  { isloading ? "لطفا کمی صبر کنید"  : "ورود" } </button>
                         </form>
                     </div>
                 </div>
             </div>
             </div>
    )
}


export default LoginPage;

