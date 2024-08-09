const delay = (ms: number) => {
    return new Promise(resolve => setTimeout(resolve, ms));
}

const createRandomPlane = (probabilityTK: number) => {

   const isTK = Math.random() * 100 < probabilityTK;
   const prefix = isTK ? "TK" : "OT";
   const randomNumbers = Math.floor(Math.random() * 10000).toString().padStart(4, '0');
   
   return prefix + randomNumbers;
}
 

export {delay,createRandomPlane};

 