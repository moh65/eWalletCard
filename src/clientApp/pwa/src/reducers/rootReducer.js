

import { authentication } from "./auth.reducer";
import { userLegal } from './nationalCode.reducer';
import { tbs } from './tbs.reducer';
import { client } from './client.reducer';
import {combineReducers} from 'redux';
import {card} from "./card.reducer";


const rootReducer = combineReducers({
    userLegal: userLegal,
    tbs:tbs,
    client:client,
    card:card,
    authentication: authentication,
});

export default rootReducer;