import encoding from 'k6/encoding';
import http from 'k6/http';
import { check } from 'k6';


export default function () {
   
    let data = { username: 'm.parsa', password: '123456' };

    let headers = { 'Content-Type': 'application/json' };
    let res = http.post(`http://mofid_wallet/api/${API_VERSION}/user/auth`, JSON.stringify(data), { headers: headers });
    console.log(res.body)
    var authKey = JSON.parse(res.body).result.token;
    
    check(res, {
        'status is 200': r => r.status == 200,
        'authentication': r => r.json().result.token && r.json().result.token.length > 0
    });
    
    
    return authKey;
}

export const API_VERSION = "v1.0";