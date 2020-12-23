export interface ResoucerTypeAvaliabilitiesResponse {
  id: number;
  name: string;
  resourceAvailabilities: Array<ResoucerTypeAvaliabilitiesResponse>;
}

export interface ResoucerTypeAvaliabilitiesResponse {
  id: number;
  start: Date;
  end: Date;
  isReccuring: boolean;
  interval: TimeSpan;
  resourceId: number;
}

export interface TimeSpan {
  totalMinutes: number;
}
