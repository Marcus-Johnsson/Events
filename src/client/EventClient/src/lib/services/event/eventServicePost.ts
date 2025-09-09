import {ApiService} from '../apiService';

export interface EventData {
    name: string;
    dateStart: Date; 
    dateEnd: Date;  
    location: string;
    description?: string;
    cateories: Categories[];
}

export interface Categories {
    id: string;
    name: string;
}

export interface EventDataReponse {
   
    name: string;
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async postEvent<T>(eventData: EventData): Promise<EventDataReponse> {
		const response = await this.apiService.post('/event', eventData);
		return response as EventDataReponse;

    }
}