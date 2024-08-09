import { useState } from "react";
import axiosClient from "../utils/axiosClient";

interface Props {

}

const Action = (props: Props) => {
    const [isNumberValid, setNumberValid] = useState(true);
    const [inputValue, setInputValue] = useState(15);

    const onStartButtonClickHandler =  async () => {
        if (inputValue < 11) {
            setNumberValid(false);

        } else {
            setNumberValid(true);
            var data = {
                code: 'TK12515',
                size: 2
            }
            setInterval(await axiosClient.post("/api/plane", data ), 1500);
            
        }
    }

    const onClearButtonClickHandler = () => {
        setInputValue(15);
        setNumberValid(true);
    }

    return (
        <div className="action-wrapper">
            <div className="card">
                <div className="card-body">
                    Simulasyon için uçak sayısı giriniz.
                </div>
            </div>
            <div className="input-group mb-3" >
                <button className="btn btn-outline-secondary" type="button" onClick={async() => await onStartButtonClickHandler()}>Başlat</button>
                <input type="number" className="form-control" aria-label="Example text with button addon" aria-describedby="button-addon1" value={inputValue} onChange={(e) => setInputValue(parseInt(e.target.value))} />
            </div>
            {
                isNumberValid == true ? null : <div className="card text-bg-danger">
                    <div className="card-body">
                        10'dan fazla uçakla simulasyonu başlatabilirsiniz.
                    </div>
                </div>
            }
            <div className="d-grid gap-2 d-md-block mt-2">
                <button className="btn btn-primary" type="button" onClick={onClearButtonClickHandler}>Simulasyonu temizle</button>
            </div>
        </div>
    );
}

export default Action;