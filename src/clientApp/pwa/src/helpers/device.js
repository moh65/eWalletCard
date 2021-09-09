export const deviceId = () => {
    let deviceId = sessionStorage.getItem("deviceId");

    if(!deviceId){
        deviceId =  "d" + Math.floor(Math.random() * 10000000000);
        sessionStorage.setItem("deviceId" , deviceId);
    }

    return deviceId;

}