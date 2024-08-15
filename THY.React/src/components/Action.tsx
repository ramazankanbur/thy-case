import { useState } from "react";
import axiosClient from "../utils/axiosClient";
import { createRandomPlane, delay } from "../utils/util"
interface Props {

}

const Action = (props: Props) => {
    const [inputValue, setInputValue] = useState(15);

    const onStartButtonClickHandler = async () => {
        for (let index = 0; index < inputValue; index++) {
            await delay(1000);
            const code = createRandomPlane(100);
            const size = Math.floor(Math.random() * 3);
            await axiosClient.post("/api/plane", { code, size });
        }
    }


    return (
        <div className="action-wrapper">
            <div className="card">
                <div className="card-body">
                Please provide the number of plane to be simulated
                </div>
            </div>
            <div className="input-group mb-3" >
                <button className="btn btn-outline-secondary" type="button" onClick={async () => await onStartButtonClickHandler()}>Start</button>
                <input type="number" className="form-control" aria-label="Example text with button addon" aria-describedby="button-addon1" value={inputValue} onChange={(e) => setInputValue(parseInt(e.target.value))} />
            </div>
        </div>
    );
}

export default Action;