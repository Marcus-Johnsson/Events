import ApiService from '../apiService';


export interface CategoriesDataReponse{
    id: number; 
    title: string;
}

export class CategoryPostService {
    constructor(private apiService: ApiService) {}

    public async GetCategories<T>(): Promise<CategoriesDataReponse[]> {
		const response = await this.apiService.get('/categories');
		return response as CategoriesDataReponse[];
    }
}