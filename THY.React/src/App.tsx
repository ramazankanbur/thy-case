import { useState, useEffect } from "react";


import { createHubConnection } from "./utils/hub";
import PlaneList from "./components/PlaneList";
import GateList from "./components/GateList";
import Action from "./components/Action";

import axiosClient from "./utils/axiosClient";
import { GateStatus, PlaneStatus } from "./models/Enum";
import "./App.css";
import { HubConnection } from "@microsoft/signalr";
import { Plane, PlaneGateApiResult, PlaneGateMessageResult } from "./models/Model";

function App() {

  const [hubConnection, setHubConnection] = useState<HubConnection>();
  const [gates, setGates] = useState<PlaneGateApiResult[]>([]);

  const [planes, setPlanes] = useState<Plane[]>([]);

  const fetchData = async () => {
    const result = await axiosClient.get("/api/PlaneGate");
    setGates(result.data);
  }

  useEffect(() => {
    (async () => {
      try {
        await fetchData();
        await createHubConnection().then(res => { setHubConnection(res) });
      } catch (error) {
        console.log(error);
      }
    })();
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on("PlaneQueued", (message: Plane) => {
        console.log("queued", message)
        setPlanes(prevData => [...prevData, { code: message.code, id: message.id, size: message.size, status: PlaneStatus.Ground }]
        );
      });

      hubConnection.on("PlaneAssigned", (message: PlaneGateMessageResult) => {
        console.log("message", message);
        setGates(prevData =>
          prevData.map(gate => (
            gate.gateId == message.gateId ? { ...gate, gateStatus: GateStatus.InUse, passengerOffboardingDuration: message.passengerOffboardingDuration } : gate
          ))
        );

        setPlanes(
          prevData =>
            prevData.map(plane => (
              plane.id == message.planeId ? { ...plane, status: PlaneStatus.OnGate } : plane
            ))
        );
      });


      hubConnection.on("GateAvailable", (message: PlaneGateMessageResult) => {
        console.log("available", message)
        setGates(prevData =>
          prevData.map(gate => (
            gate.gateId == message.gateId ? { ...gate, gateStatus: GateStatus.Available } : gate
          ))
        );
        
        setPlanes(
          prevData => prevData.filter(plane => plane.id !== message.planeId)
        );
      });



    }
  }, [hubConnection])
 
  return (
    <>
      <div className="container">

        <div className="plane-list-container">
          <PlaneList planes={planes} /> 
        </div>

        <div className="action-container">
          <Action />
        </div>

        <div className="gate-list-container">
          <GateList gates={gates} />
        </div>
        
      </div>
    </>
  )
}

export default App
