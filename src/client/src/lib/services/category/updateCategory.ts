import ApiService from "$lib/services/apiService";
import { EventPostService } from "$lib/services/category/PatchCategory";
import type { UpdateCategoriesData } from "$lib/services/category/PatchCategory";

const apiService = new ApiService();

const eventPostService = new EventPostService(apiService);

export async function updateCategory(data: UpdateCategoriesData) {
    await eventPostService.putEvent(data);
}
