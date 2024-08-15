import { PlaneGateApiResult } from "../models/Model";
import { GateStatus, Sizes } from "../models/Enum";

import Countdown from "./Countdown";

interface Props {
    gates: PlaneGateApiResult[]
};

const GateList = (props: Props) => {
    console.log(props.gates)
    return (
        <>
            <ul className="list-group">
                {props.gates.map((item, index) => ( 
                    item.gateStatus !== GateStatus.InUse ?
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-success">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.gateCode} / {Sizes[item.gateSize]}</div>
                                Location: <i>({item.gateLocation})</i>
                            </div>
                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[0]}
                            </button> 
                        </li> :
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-danger">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.gateCode} / {Sizes[item.gateSize]}</div>
                                Location: <i>({item.gateLocation})</i>
                            </div>

                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[item.gateStatus]} <span className="badge text-bg-secondary"><Countdown gateId={item.gateId} start={item.passengerOffboardingDuration} /> sec</span>
                            </button>
                        </li>
                ))}
            </ul>
        </>
    );
}

export default GateList;