import ApiService from '../apiService';



export interface CategoryDataReponse {
    name: string;
}

export class CategoryPostService {
    constructor(private apiService: ApiService) {}

    public async postCategory<T>(eventData: string): Promise<CategoryDataReponse> {
		const response = await this.apiService.post('/categories', eventData);
		return response as CategoryDataReponse;

    }
}