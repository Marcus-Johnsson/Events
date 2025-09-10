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

<table>
    <thead>
        <tr>
            <th>Categories</th>
        </tr>
    </thead>
    <tbody>
        {#each categories as category}
            <tr style="background-color: {category.id % 2 === 0 ? 'rgb(95, 62, 62)' : 'rgb(124, 100, 56)'}">
                <td>{category.id}. {category.title}</td>

                <td>Ändra</td>
                <td>Ta bort</td>
            </tr>
            
        {/each}
    </tbody>
</table>

<style>
      th {
    width: 25%;
    background-color: rgb(126, 95, 41);
    color: rgb(24, 23, 23)!important;
    
  }
  table, th, td {
    border: 1px solid rgb(58, 55, 55);
    padding: 10px;
    max-width: 800px;
    
  }
  td {
    text-align: center;
    min-height: 40px;
    min-width: 10px;
  }
  tr {
    height: 40px;
    color: rgb(206, 202, 202);
  }
</style>