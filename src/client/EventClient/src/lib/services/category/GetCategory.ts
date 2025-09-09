import {ApiService} from '../apiService';

export interface CategoriesData {
    id: string;
    name: string;
}

export interface EventDataReponse {
    Id: number; 
    name: string;
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async GetEvent<T>(): Promise<EventDataReponse> {
		const response = await this.apiService.get('/category');
		return response as EventDataReponse;
    }
}