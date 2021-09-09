import './App.css';
import  HomePage  from "./Pages/HomePage";
import  LoginPage  from "./Pages/LoginPage";
import  TemporaryClientPage  from "./Pages/TemporaryClientPage";
import Header from "./components/Header";
import Footer from "./components/Footer";
import  ClientPage  from "./Pages/ClientPage";
import  InfoPage  from "./Pages/InfoPage";
import  SettingPage  from "./Pages/SettingPage";
import  LogPage  from "./Pages/LogPage";
import { PrivateRoute } from "./components/PrivateRoute";
import {  BrowserRouter as Router, Route , Switch } from 'react-router-dom';
import { connect } from 'react-redux';
import { history } from './helpers/history';
import './css/bootstrap.min.css'
import { useEffect } from "react";
import { useSelector } from "react-redux";


function App() {

  const user = useSelector(state => state.authentication.user);

  
    useEffect(() => {
      if (!user) {
          history.push('/login');
      }

    }, [user])

  return (
    <div className="App">
                  <Router history={history}>
                    <Switch>
                        <Route path='/login' component={PublicLayout} />
                        <Route path='/' component={ProtectedLayout} />
                    </Switch>
                  </Router>
    </div>
  );
}


export const PublicLayout = (props) => <div>

  <Switch>
     <Route path="/login" component={LoginPage} />
  </Switch>

</div>

export const ProtectedLayout = (props) => <div>
<Header/>
 <Switch>
    <PrivateRoute exact path="/" component={TemporaryClientPage} />
    
    <PrivateRoute  path="/client" component={ClientPage} />
    <PrivateRoute  path="/info/:id?" component={InfoPage} />
    <PrivateRoute  path="/setting" component={SettingPage} />
    <PrivateRoute  path="/logs" component={LogPage} />
    <PrivateRoute path='*' exact={true} component={ClientPage} />
 </Switch>
 <Footer />
</div>

export default App;
