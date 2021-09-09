import encoding from 'k6/encoding';
import http from 'k6/http';
import { check, sleep } from 'k6';
import test from './test.js';

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
    }
    let req = http.post(`http://mofid_wallet/api/${API_VERSION}/card/register`, JSON.stringify(authData), { headers: authHeaders });
 //   console.log(req.body);
    check(req, {
        'card register': r => r.json().statusCode == 200 
    });
}