import * as signalR from "@microsoft/signalr";
import {  HubConnectionBuilder } from "@microsoft/signalr";

import { HUB_CONNECTION } from "./config";


const createHubConnection = async () => {

    const hubConnection = new HubConnectionBuilder().withUrl(HUB_CONNECTION, { skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets }).build();
    try {
        await hubConnection.start();
        console.log("connected"); 
    } catch (error) {
        console.log("hata", error);
    }
    return hubConnection;
}

export { createHubConnection }; 