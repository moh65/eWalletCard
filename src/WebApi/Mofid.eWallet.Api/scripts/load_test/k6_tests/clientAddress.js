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

    let addressParam = '?nationalCode=0011813563'
    let addressRes = http.get(`http://mofid_wallet/api/${API_VERSION}/client/address` + addressParam, { headers: authHeaders });
   // console.log(addressRes.body);
    check(addressRes, {
        'address': r => r.json().result.address.length > 0
    });
}