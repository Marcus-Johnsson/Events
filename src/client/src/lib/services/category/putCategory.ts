import ApiService from '../apiService';

export interface UpdateCategoriesData {
    id: number;
    title: string;
}

export interface CatagoryDataReponse {
    name: string;
}

export class EventPostService {
    constructor(private apiService: ApiService) {}

    public async putCategory<T>(categoryData: UpdateCategoriesData): Promise<CatagoryDataReponse> {
        const response = await this.apiService.put('/categories/'+ categoryData.id, categoryData);
        return response as CatagoryDataReponse;
    }
}