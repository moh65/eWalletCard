import { useSelector , useDispatch } from "react-redux";
import { userActions  } from "../actions/user.action";
import { useHistory  } from "react-router-dom";
export const AuthInfo = () => {
    const user = useSelector(state => state.authentication.username)
    const dispatch = useDispatch();
    const history = useHistory();
    const logOut = ()=> {
        dispatch(userActions.logout());
        history.push('/login')
    }
    return (
        <>
            <span>{user}</span>
            <span onClick={logOut}> خروج از سیستم </span>
        </>  
    )
}