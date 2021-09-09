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
        nationalCode: "0011234563",
        phoneNumber: "09123456789",
        nationalCardSerial: "11223344",
        deviceId: "9123456789",
        postalCode: "1234567890",
        AddressString: "تهران",
    }
    let kycResult = http.post(`http://mofid_wallet/api/${API_VERSION}/client/kyc`, JSON.stringify(authData), { headers: authHeaders });
    check(kycResult, {
        'kyc': r => r.json().statusCode == 200 
    });
}