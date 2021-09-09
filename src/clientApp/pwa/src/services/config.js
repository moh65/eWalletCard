export const baseUrl = process.env.API_URL || process.env.REACT_APP_API_URL;
export const authUrl = baseUrl + "v1/user/auth";

export  const userAuth = {
    username : process.env.REACT_APP_API_USERNAME,
    password : process.env.REACT_APP_API_PASSWORD,
}