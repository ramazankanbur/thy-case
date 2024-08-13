import { GatePlaneResult } from "../models/Model";
import { GateStatus, Sizes } from "../models/Enum";

import Countdown from "./Countdown";

interface Props {
    gates: GatePlaneResult[]
};

const GateList = (props: Props) => {
    return (
        <>
            <ul className="list-group">
                {props.gates.map((item, index) => ( 
                    item.GateStatus !== GateStatus.InUse ?
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-success">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.GateCode} / {Sizes[item.GateSize]}</div>
                                Location: <i>({item.GateLocation})</i>
                            </div>
                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[0]}
                            </button> 
                        </li> :
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-danger">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.GateCode} / {Sizes[item.GateSize]}</div>
                                Location: <i>({item.GateLocation})</i>
                            </div>

                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[item.GateStatus]} <span className="badge text-bg-secondary"><Countdown start={item.PassengerOffboardinnDuration} /> sn</span>
                            </button>
                        </li>
                ))}
            </ul>
        </>
    );
}

export default GateList;