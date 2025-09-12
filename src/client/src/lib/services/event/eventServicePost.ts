import ApiService from '../apiService';

export interface EventData {
    title: string;
    dateStart: Date; 
    dateEnd: Date;  
    location: string;
    description?: string;
    categories: number[];
}

export interface EventDataReponse {
    name: number;
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async postEvent<T>(eventData: EventData): Promise<EventDataReponse> {
		const response = await this.apiService.post('/events', eventData);
		return response as EventDataReponse;

    }
}