import axios from 'axios';

const axiosClient = axios.create({ baseURL: 'http://localhost:5108/', timeout: 5000, headers: { 'Content-Type': 'application/json', Accept: 'application/json',  'Access-Control-Allow-Origin' : '*',
'Access-Control-Allow-Methods':'GET,PUT,POST,DELETE,PATCH,OPTIONS' } });

axiosClient.defaults.baseURL = 'http://localhost:5108/';

export default axiosClient;