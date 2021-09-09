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
        deviceId: "112344124",
        phoneNumber: "09123456789",
        otp: "123456"
    }
    let verifyResult = http.post(`http://mofid_wallet/api/${API_VERSION}/client/verify`, JSON.stringify(authData), { headers: authHeaders });
    check(verifyResult, {
        'verify': r => r.json().statusCode == 200 
    });
}