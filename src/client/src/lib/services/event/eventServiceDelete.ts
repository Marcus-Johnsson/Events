import ApiService from '../apiService';

export interface EventDeleteData {
    id: number;
}

export class EventDeleteService {
    constructor(private apiService: ApiService) {}

    public async deleteEvent(eventData: EventDeleteData): Promise<EventDeleteData> {
        const response = await this.apiService.delete(`/events/${eventData.id}`);
        return response as EventDeleteData;
    }
}