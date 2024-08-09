import { useState, useEffect } from "react";


import { createHubConnection } from "./utils/hub";
import PlaneList from "./components/PlaneList";
import GateList from "./components/GateList";
import Action from "./components/Action";

import axiosClient from "./utils/axiosClient";
import { PlaneStatus, Sizes } from "./models/Enum";
import "./App.css";
import { HubConnection } from "@microsoft/signalr";

function App() {

  const [hubConnection, setHubConnection] = useState<HubConnection>();
  const [gates, setGates] = useState([]);

  const [showHideValue, setShowHideValue] = useState(false);
  const [planes, setPlanes] = useState([
    { code: "TK001", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK002", id: "s2dd342", size: Sizes.M, status: PlaneStatus.OnGate },
    { code: "TK003", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK004", id: "s2dd342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK005", id: "s2342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK006", id: "s2dd342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK007", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK008", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK009", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK0010", id: "s2dd342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK0011", id: "s2342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK0012", id: "s2dd342", size: Sizes.L, status: PlaneStatus.OnGate },
    { code: "TK0013", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK0014", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
    { code: "TK0015", id: "s2342", size: Sizes.L, status: PlaneStatus.OnGate },
  ]);


  const fetchData = async () => {
    const result = await axiosClient.get("/api/gate");
    setGates(result.data);
  }

  useEffect(() => {
    createHubConnection().then(res => { setHubConnection(res) });
    fetchData();
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on("PlaneQueued", (message) => {
        console.log("queued",message);
      });

      hubConnection.on("PlaneAssigned", (message) => {
        console.log("assigned",message);
      });
    }
  }, [hubConnection])

  const onShowHideChangeHandler = () => {
    console.log(showHideValue);
    setShowHideValue(!showHideValue);
    if (!showHideValue) {
      const planeSet = planes.filter(function (plane) {
        return plane.status == PlaneStatus.Ground;
      });

      setPlanes(planeSet);
    } else {
      setPlanes([
        { code: "TK001", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
        { code: "TK002", id: "s2dd342", size: Sizes.M, status: PlaneStatus.OnGate },
        { code: "TK003", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
        { code: "TK004", id: "s2dd342", size: Sizes.L, status: PlaneStatus.Ground },
        { code: "TK005", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
        { code: "TK006", id: "s2dd342", size: Sizes.L, status: PlaneStatus.OnGate },
        { code: "TK007", id: "s2342", size: Sizes.L, status: PlaneStatus.Ground },
      ]);
    }
  }

  return (
    <>
      <div className="container">
        <div className="plane-list-container">
          <PlaneList planes={planes} />
          <div className="form-check form-switch">
            <input className="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckDefault" onChange={onShowHideChangeHandler} />
            <label className="form-check-label" htmlFor="flexSwitchCheckDefault">Eşleşenleri gizle</label>
          </div>
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
