import {ApiService} from '../apiService';

export interface UpdateCategoriesData {
    name: string;
}

export interface EventDataReponse {
    name: string;
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async patchEvent<T>(eventData: UpdateCategoriesData): Promise<EventDataReponse> {
        const response = await this.apiService.put('/category', eventData);
        return response as EventDataReponse;
    }
}