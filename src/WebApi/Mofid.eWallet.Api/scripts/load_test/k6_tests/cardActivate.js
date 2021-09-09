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
        phoneNumber: "09123876459",
        NationalCode: "123456",
        Otp: '123',
    }
    let addressRes = http.post(`http://mofid_wallet/api/${API_VERSION}/card/activate`, JSON.stringify(authData), { headers: authHeaders });
     console.log(addressRes.body);
    check(addressRes, {
        'card activate': r => r.json().statusCode == 200 || r.json().StatusCode == 500
    });
}