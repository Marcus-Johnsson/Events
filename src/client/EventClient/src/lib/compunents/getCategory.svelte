<script>
    import ApiService from "$lib/services/apiService";
    import { CategoryPostService } from "$lib/services/category/GetCategory";
    import { onMount } from "svelte";

    const apiService = new ApiService();
    let categories = [];
    

    onMount(async () => {
    categories = new CategoryPostService(apiService);
    });

</script>

<div>
    {#each categories as category}
        <div>{category.title}</div>
    {/each}
</div>