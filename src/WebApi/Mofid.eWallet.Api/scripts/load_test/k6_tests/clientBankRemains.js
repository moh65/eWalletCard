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
        'Content-Type': 'application/json', 'Authorization': `Bearer ${auth}` };

    let authData = {
        NationalCode: "0011813563",
        deviceId: "9123876459",
    }
    let addressRes = http.get(`http://mofid_wallet/api/${API_VERSION}/client/BankRemain?nationalCode=${authData.nationalCode}&deviceId=${authData.deviceId}`, { headers: authHeaders });
    
    check(addressRes, {
        'client status': r => r.json().statusCode == 200 ,
        'client bank remain': r => r.json().result.remain.length > 0 
    });
}