import { Sizes, GateStatus, PlaneStatus } from "./Enum";

interface Plane {
  id: string,
  code: string,
  size: Sizes,
  status: PlaneStatus
}

interface Gate {
  gateStatus: number,
  id: string,
  code: string,
  size: Sizes,
  location: string, 
}

interface PlaneGate {
  PlaneId: string,
  GateId: string
}

interface GatePlaneResult {
  GateCode: string,
  GateId: string,
  GateSize: number,
  GateLocation: string,
  GateStatus: number,
  PassengerOffboardinnDuration: number
}

export type { Plane, Gate, PlaneGate, GatePlaneResult };