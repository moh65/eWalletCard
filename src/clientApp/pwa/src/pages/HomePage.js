import ClipLoader from "react-spinners/ClipLoader";
import {useState, useEffect} from "react";
import {useDispatch, useSelector} from "react-redux";
import {userActions} from "../actions/user.action";
import {userAuth} from "../services/config";
import { useHistory } from "react-router-dom";

import logo from '../logo.png'

const HomePage = () => {
    const history = useHistory();
    let [color, setColor] = useState("#000");
    const dispatch = useDispatch();
    const user = useSelector(state => state.authentication.user);
    useEffect(() => {
        console.log(user);
        dispatch(userActions.login(userAuth.username, userAuth.password));
    })

    useEffect(() => {
        if (user) {
           history.push("/nationalcode")
        }
    }, [user]);

    return (
        <div className="conatiner" style={{
            height: "100vh",
            display: "flex",
            justifyContent: "center",
            alignItems: "center"
        }}>


            <img src={logo} width={100} />
            <ClipLoader color={color} loading={true} size={50}/>
        </div>
    )
}

export default HomePage;

