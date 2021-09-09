import { userService } from '../services/user.service';
const tokenExpire = () => {
    return dispatch  => {
        localStorage.removeItem('user');
        localStorage.removeItem('username');

        dispatch({ type: userService.logout  });
    }
}

export default tokenExpire;