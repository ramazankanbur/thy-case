import { useState, useEffect } from 'react';

interface Props {
    start: number
};

const Countdown = (props: Props) => {
    const [count, setCount] = useState(props.start);

  useEffect(() => {
    if (count <= 0) return;  

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