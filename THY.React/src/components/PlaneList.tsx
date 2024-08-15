import { Plane } from "../models/Model";
import { PlaneStatus, Sizes } from "../models/Enum";

interface Props {
  planes: Plane[]
};

const PlaneList = (props: Props) => {

  return (
    <>
      <ul className="list-group">
        {props.planes.map((item, index) => (
          item.status === PlaneStatus.OnGate ?
            <li key={index} className="list-group-item d-flex justify-content-between list-group-item-action list-group-item-success"><b>{item.code} / {Sizes[item.size]}</b> <span className="badge bg-primary rounded-pill">{PlaneStatus[item.status]}</span></li> :
            <li key={index} className="list-group-item d-flex justify-content-between list-group-item-action list-group-item-danger"><b>{item.code} / {Sizes[item.size]}</b> <span className="badge bg-primary rounded-pill">{PlaneStatus[item.status]}</span></li>
        ))}
      </ul>

    </>
  );
}

export default PlaneList;