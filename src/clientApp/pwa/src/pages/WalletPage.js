import "../content/css/Wallet.css"
import Tbs from '../components/Wallet/Tbs'
import MofidCard from '../components/Wallet/MofidCard'
import UnRegister from '../components/Wallet/UnRegister'

import { useEffect } from 'react'

import TransactionsLink from "../components/Wallet/TransactionsLink";
import {useSelector ,  useDispatch} from "react-redux";
import ClipLoader from "react-spinners/ClipLoader";
import {clientActions} from "../actions/client.action";
import InActiveCard from "../components/Wallet/InActiveCard";
import States from "../components/Wallet/States";
const WalletPage = () => {
    const dispatch = useDispatch();
    const userstates = useSelector(state => state.client?.states);
    const { nationalCode } = useSelector(state => state.userLegal.data);
    const isLoading = useSelector(state => state.client?.isLoading);

    useEffect(() => {
        if(nationalCode)
            dispatch(clientActions.getStates(nationalCode));
    } , [nationalCode]);

    return (
        <div className="">
            <br/>
            <br/>
            <div className="row tbs-container"  style={{paddingBottom:20}}>
                <Tbs />
                <br />
            </div>

            {isLoading &&  <ClipLoader color={"#000"} loading={true} size={50}/>}
            {!isLoading && userstates && userstates.indexOf("CardIssued") > -1 && userstates.indexOf("CardActivated") <= -1 && <>

                <div>
                    <States />
                    <br />
                    <InActiveCard />
                </div>

            </> }
            {!isLoading &&  userstates && userstates.indexOf("CardActivated") > -1 && <>
                <MofidCard />
                <br />
                <br />
                <TransactionsLink />
            </> }

            {!isLoading &&  userstates.indexOf("CardIssued") <= -1 && <>
                <br />
                <br />
                <UnRegister />
            </> }


        </div>
    )
}

export  default  WalletPage;