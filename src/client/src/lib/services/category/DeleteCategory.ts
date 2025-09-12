import ApiService from '../apiService';

export interface CategoryDeleteData {
    id: number;
}

export class CategoryDeleteService {
    constructor(private apiService: ApiService) {}

    public async deleteCategory(categoryData: CategoryDeleteData): Promise<CategoryDeleteData> {
        const response = await this.apiService.delete(`/categories/${categoryData.id}`);
        return response as CategoryDeleteData;
    }
}