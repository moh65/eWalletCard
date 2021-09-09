import encoding from 'k6/encoding';
import http from 'k6/http';
import { check, sleep } from 'k6';
import test, { API_VERSION } from './test.js';

export function setup() {
    var auth = test();
    return auth;
}

export default function (auth) {
    let authHeaders = {
        'Content-Type': 'application/json', 'Authorization': `Bearer ${auth}`
    };

    let _Data = {
        nationalCode: "4900729671",
    }
    let addressRes = http.get(`http://mofid_wallet/api/${API_VERSION}/client/IsLegal?nationalCode=${_Data.nationalCode}`, { headers: authHeaders });
    console.log(addressRes.body);
    check(addressRes, {
        'client status': r => r.json().statusCode == 200,
        'client isLegal': r => r.json().result == true || r.json().result == false
    }); 
}