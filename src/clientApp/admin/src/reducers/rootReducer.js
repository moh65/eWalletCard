

import { authentication } from "./auth.reducer";
import { temporaryClient } from "./tclient.reducer";
import { client } from "./client.reducer";
import { info } from "./info.reducer";
import { log } from "./log.reducer";
import {combineReducers} from 'redux';


const rootReducer = combineReducers({
    log:log,
    info : info,
    client: client,
    authentication: authentication,
    temporaryClient:temporaryClient,
   
});

export default rootReducer;