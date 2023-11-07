import { Sizes, GateStatus, PlaneStatus } from "./Enum";

interface Plane {
  id: string,
  code: string,
  size: Sizes,
  status: PlaneStatus
}

interface Gate {
  status: GateStatus,
  id: string,
  code: string,
  size: Sizes,
  location: string
}

interface PlaneGate {
  PlaneId: string,
  GateId: string
}

export type { Plane, Gate, PlaneGate };