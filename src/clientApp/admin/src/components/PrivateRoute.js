import { Route, Redirect } from 'react-router-dom';

export const PrivateRoute = ({ component: Component, ...rest }) =>  {
    
    const user = localStorage.getItem('user');
    return(
        
    <Route {...rest} render={props => (
        user
            ? <Component {...props} />
            : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
    )} />
)
}