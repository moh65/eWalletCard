import { Route, Redirect } from 'react-router-dom';
import {useSelector} from "react-redux";

export const PrivateRoute = ({ component: Component, ...rest }) =>  {
    
    const user = useSelector(state => state.authentication.user)

    return(
        
    <Route {...rest} render={props => (
        user
            ? <Component {...props} />
            : <Component {...props} />
        // <Redirect to={{pathname: '/token-refresh', state: {from: props.location}}} />
    )} />
)
}