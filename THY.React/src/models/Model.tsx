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
  location: string
}

interface PlaneGateMessageResult {
  planeId: string,
  gateId: string,
  passengerOffboardingDuration: number,
  planeStatus: PlaneStatus,
  gateStatus: GateStatus
}

interface PlaneGateApiResult {
  gateCode: string,
  gateId: string,
  gateSize: number,
  gateLocation: string,
  gateStatus: number,
  passengerOffboardingDuration: number
}

export type { Plane, Gate, PlaneGateMessageResult, PlaneGateApiResult };