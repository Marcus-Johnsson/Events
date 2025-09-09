import {ApiService} from '../apiService';

export interface categoryDeleteData {
    id: number;
}

export interface categoryDataReponse {
    Id: number; 
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async deleteEvent<T>(categoryData: categoryDeleteData): Promise<categoryDeleteData> {
        const response = await this.apiService.delete(`/category//${categoryData}`);
        return response as categoryDeleteData;
    }
}