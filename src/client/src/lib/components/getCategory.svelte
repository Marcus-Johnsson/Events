<script lang="ts">
    import ApiService from "$lib/services/apiService";
    import { CategoryPostService } from "$lib/services/category/GetCategory";
    import { onMount } from "svelte";

    const apiService = new ApiService();
    
    interface Category{
        title: string;
        id: number;
    }
    let categories: Category[] = [];

    onMount(async () => {
    categories = await new CategoryPostService(apiService).GetCategories();
    });

</script>

<div>
    {#each categories as category}
        <div>{category.id}. {category.title}</div>
    {/each}
</div>