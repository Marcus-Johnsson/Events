import ApiService from '../apiService';



export interface CategoryDataResponse {
    name: string;
}

export class CategoryPostService {
    constructor(private apiService: ApiService) {}

    public async postCategory<T>(categoryData: string): Promise<CategoryDataResponse> {
		const response = await this.apiService.post('/categories', categoryData);
		return response as CategoryDataResponse;

    }
}