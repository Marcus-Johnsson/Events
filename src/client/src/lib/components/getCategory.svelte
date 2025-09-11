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
        <div class="category"
        
        >{category.id}. {category.title}</div>
    {/each}
</div>

<style>
    .category {
        margin: 10px 0;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        background-color: #f9f9f9;
        width: fit-content;
    }
</style>