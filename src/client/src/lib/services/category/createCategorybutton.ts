    import {CategoryPostService} from "$lib/services/category/PostCategory"
    import ApiService from "$lib/services/apiService"

    const apiService = new ApiService;
    const categoryPostService = new CategoryPostService(apiService);

export async function createCategory(categoryData: string) {
    if (categoryData.trim() === "New category" || categoryData.trim() === "") return;

    await categoryPostService.postCategory(categoryData);
}

