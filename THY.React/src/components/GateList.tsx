import { Gate } from "../models/Model";
import { GateStatus, Sizes } from "../models/Enum";

interface Props {
    gates: Gate[]
};

const GateList = (props: Props) => {
    return (
        <>
            <ul className="list-group">
                {props.gates.map((item, index) => (
                    item.status !== GateStatus.InUse ?
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-success">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.code} / {Sizes[item.size]}</div>
                                Location: <i>({item.location})</i>
                            </div>
                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[0]}
                            </button> 
                        </li> :
                        <li key={index} className="list-group-item d-flex justify-content-between align-items-start list-group-item-danger">
                            <div className="ms-2 me-auto">
                                <div className="fw-bold">{item.code} / {Sizes[item.size]}</div>
                                Location: <i>({item.location})</i>
                            </div>

                            <button type="button" className="btn btn-primary btn-sm disabled">
                            {GateStatus[item.status]} <span className="badge text-bg-secondary">0sn</span>
                            </button>
                        </li>
                ))}
            </ul>

        </>
    );
}

export default GateList;