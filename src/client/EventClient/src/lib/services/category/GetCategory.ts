import ApiService from '../apiService';


export interface CategoriesDataReponse{
    id: number; 
    title: string;
}

export class CategoryPostService {
    constructor(private apiService: ApiService) {}

    public async GetEvent<T>(): Promise<CategoriesDataReponse> {
		const response = await this.apiService.get('/category');
		return response as CategoriesDataReponse;
    }
}