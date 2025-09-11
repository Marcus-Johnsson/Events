import ApiService from "$lib/services/apiService";
import { EventPostService } from "$lib/services/category/putCategory";
import type { UpdateCategoriesData } from "$lib/services/category/putCategory";

const apiService = new ApiService();

const eventPostService = new EventPostService(apiService);

export async function updateCategory(data: UpdateCategoriesData) {
    await eventPostService.putCategory(data);
}
