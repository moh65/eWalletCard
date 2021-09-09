import encoding from 'k6/encoding';
import http from 'k6/http';
import { check, sleep } from 'k6';
import AuthKey from './authKey.js';
import test, { API_VERSION } from './test.js'


export function setup() {
    return test();
}

export default function (auth) {

    let authData = {
        phoneNumber: "09123876459",
            deviceId: "9123876459",
                nationalCode: "",
                    otp: ""
    }

    let authHeaders = {
        'Content-Type': 'application/json', 'Authorization': `Bearer ${auth}` };
    let res2 = http.post(`http://mofid_wallet/api/${API_VERSION}/token`, JSON.stringify(authData), { headers: authHeaders });
    
    check(res2, {
        'token': r => r.json().result.accessToken.length > 0
    });

}