export interface ResoucerTypeAvaliabilitiesResponse {
  id: number;
  name: string;
  resourceAvailabilities: Array<ResoucerTypeAvaliabilitiesResponse>;
}

export interface ResoucerTypeAvaliabilitiesResponse {
  id: number;
  start: string;
  end: string;
  isReccuring: boolean;
  interval: any;
  resourceId: number;
}
