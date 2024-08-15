import { useState, useEffect } from 'react';
import axiosClient from "../utils/axiosClient";


interface Props {
    start: number,
    gateId: string
};

const Countdown = (props: Props) => {
    const [count, setCount] = useState(props.start);

  useEffect(() => {
    if (count <= 0) { 
        (async  () => await axiosClient.post("/api/Gate/MakeGateAvailable", { "gateId": props.gateId}))();
        return;  
    }

    const timer = setInterval(() => {
      setCount(prevCount => prevCount - 1);
    }, 1000);

    return () => clearInterval(timer);  
  }, [count]); 

  return (
   <> {count} </> 
  );
};

export default Countdown;