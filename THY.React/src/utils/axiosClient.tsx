import axios from 'axios';

const apiUrl = "http://localhost:5000/"; 

const axiosClient = axios.create({ baseURL: apiUrl, timeout: 5000, headers: { 'Content-Type': 'application/json', Accept: 'application/json',  'Access-Control-Allow-Origin' : '*',
'Access-Control-Allow-Methods':'GET,PUT,POST,DELETE,PATCH,OPTIONS' } });

axiosClient.defaults.baseURL =  apiUrl;

export default axiosClient;