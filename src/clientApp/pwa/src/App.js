import logo from './logo.svg';
import './App.css';
import HomePage from "./pages/HomePage";
import NationalCodePage from "./pages/NationalCodePage";
import WalletPage from "./pages/WalletPage";
import VerifyActivePage from "./pages/VerifyActivePage";

import KycPage from "./pages/KycPage";
import { PrivateRoute } from "./components/PrivateRoute";
import {  BrowserRouter as Router, Route , Switch } from 'react-router-dom';
import {useSelector} from "react-redux";
import { useHistory } from 'react-router-dom'
import {useEffect} from "react";
import ContractPage from "./pages/ContractPage";
import OtpKycPage from "./pages/OtpKycPage";
import ActivePage from "./pages/ActivePage";
import usePWA from 'react-pwa-install-prompt'
function App() {
    const  history = useHistory();
    const { isStandalone, isInstallPromptSupported, promptInstall } = usePWA()
    const user = useSelector(state => state.authentication.user);

    const onClickInstall = async () => {
        const didInstall = await promptInstall()
        if (didInstall) {
            // User accepted PWA install
        }
    }




  return (
      <div className="App">
        <Router history={history}>
          <Switch>
              <PrivateRoute  path="/nationalcode/:afterLogin?" component={NationalCodePage} />
              <PrivateRoute  path="/preview" component={WalletPage} />
              <PrivateRoute  path="/contract" component={ContractPage} />
              <PrivateRoute  path="/kyc" component={KycPage} />
              <PrivateRoute  path="/kycOtp" component={OtpKycPage} />
              <PrivateRoute  path="/active" component={ActivePage} />
              <PrivateRoute  path="/verifyactive" component={VerifyActivePage} />
              <Route exact path='/' component={HomePage} />
          </Switch>
        </Router>
      </div>
  );
}




export default App;
